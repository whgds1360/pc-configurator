using System.Collections.Generic;
using Dapper;
using MySqlX.XDevAPI.Common;
using PcConfigurator.helpers.TableStructures;
using PcConfigurator.shared.DataBase;

namespace PcConfigurator.helpers.Compatibility;

enum Status
{
    OK,
    Error,
    Warning,
    Empty
};

public class AnswerTemplate
{
    public string? ConnectorType { get; set; }
    public int ConnectorCount { get; set; }
}

/// <summary>
/// Класс с проверками совместимости: 
/// 
/// 1. По чипсету проверяется CPU <-> Motherboard
/// 2. По отводимому теплу кулера и TDP проца
/// 3. По сокету кулера и TDP проца
/// 4. По форм фактору матери и корпуса
/// 5. По количеству требующихся кабелей для GPU и тому сколько имеет БП
/// 6. По типу DDR модуля памяти и матери
/// </summary>
internal class CompatibilityManager
{
    public static Status status { get; set; } = Status.Empty;
    public static string? comment { get; set; } = null;

    public static bool CheckChipset(Motherboard selectedMotherboard, Cpu selectedCpu)
    {
        using (var connect = DataBaseManager.GetConnection())
        {
            if (connect.QueryFirstOrDefault(@"SELECT * FROM cpucompatibility 
                WHERE CpuId = @SelectedCpuId 
                AND Chipset = @ChipsetSelectedMotherboard", 
                new {SelectedCpuId = selectedCpu.Id, ChipsetSelectedMotherboard = selectedMotherboard.Id}) is not null)
            {
                return true;
            }

            return false;
        }
    }

    public static bool CheckCooling(Gpu selectedGpu, Cpu selectedCpu, PowerUnit selectedPowerUnit)
    {
        using (var connect = DataBaseManager.GetConnection())
        {
            var totalСonsumption = selectedGpu.TDP + selectedCpu.TDP + 100;

            if ((totalСonsumption + totalСonsumption*0.3) <= selectedPowerUnit.Power)
            {
                return true;
            }

            return false;
        }
    }

    public static bool CheckSoketCooling(Motherboard selectedMotherboard, Cooler selectedCooler)
    {
        using (var connect = DataBaseManager.GetConnection())
        {
            if (selectedCooler.Socket == selectedMotherboard.Socket)
            {
                return true;
            }

            return false;
        }
    }

    public static bool CheckFormFactor(Motherboard selectedMotherboard, PcCase selectedPccase)
    {
        using (var connect = DataBaseManager.GetConnection())
        {
            if (connect.QueryFirstOrDefault(@"SELECT * FROM pccasecompatibility 
                WHERE CaseId = @SelectedCaseId 
                AND MotherboardFormFactor = @ChipsetSelectedMotherboard", 
                new {SelectedCpuId = selectedPccase.Id, ChipsetSelectedMotherboard = selectedMotherboard.FormFactor}) is not null)
            {
                return true;
            }

            return false;
        }
    }

    public static bool CheckGpuCables(PowerUnit selectedPowerUnit, Gpu selectedGpu)
    {
        Dictionary<string, int> gpurequiredata = new();
        Dictionary<string, int> powerunithasdata = new();

        using (var connect = DataBaseManager.GetConnection())
        {
            var gpurequire = connect.Query<AnswerTemplate>(@"SELECT ConnectorType, ConnectorCount FROM gpupowerrequirement 
                WHERE GpuId = @SelectedGpuId",
                new {SelectedPowerUnitId = selectedGpu.Id});

            foreach (var obj in gpurequire)
            {
                gpurequiredata[$"{obj.ConnectorType}"] = obj.ConnectorCount;
            }


            var powerunithas = connect.Query<AnswerTemplate>(@"SELECT ConnectorType, ConnectorCount FROM psuconnector 
                WHERE PowerUnitId = @SelectedPowerUnitId",
                new {SelectedPowerUnitId = selectedPowerUnit.Id});

            foreach (var obj in powerunithas)
            {
                powerunithasdata[$"{obj.ConnectorType}"] = obj.ConnectorCount;
            }

            bool result = true;

            foreach (var pair in gpurequiredata)
            {
                if (pair.Value != powerunithasdata[pair.Key])
                {
                    result = false;
                }
            }

            return result;
        }
    }

    public static bool CheckDdrType(Ram selectedRam, Motherboard selectedMotherboard)
    {
        if (selectedRam.DdrType == selectedMotherboard.DdrType)
        {
            return true;
        }
        return false;
    }

}