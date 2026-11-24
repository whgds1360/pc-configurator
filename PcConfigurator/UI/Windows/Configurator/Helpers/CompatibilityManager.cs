using PcConfigurator.Shared.Models;
using PcConfigurator.Shared;

namespace PcConfigurator.Windows.Configurator.Helpers;

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

    public static async Task<bool> CheckChipset(Motherboard selectedMotherboard, Cpu selectedCpu)
    {
        using (var connect = DataBaseManager.GetConnection())
        {
            try
            {
                if (await connect.QueryFirstOrDefaultAsync(@"SELECT * FROM cpu_compatibility 
                    WHERE cpu_id = @SelectedCpuId 
                    AND chipset = @ChipsetSelectedMotherboard", 
                    new {SelectedCpuId = selectedCpu.Id, ChipsetSelectedMotherboard = selectedMotherboard.ChipSet}) is not null)
                {
                    return true;
                }

                return false;
            }

            catch (MySqlException) { throw; }
        }
    }

    public static bool CheckPowerTDP(Gpu selectedGpu, Cpu selectedCpu, PowerUnit selectedPowerUnit)
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

    public static async Task<bool> CheckFormFactor(Motherboard selectedMotherboard, PcCase selectedPccase)
    {
        using (var connect = DataBaseManager.GetConnection())
        {
            try
            {
               if (await connect.QueryFirstOrDefaultAsync(@"SELECT * FROM pc_case_compatibility 
                WHERE case_id = @SelectedCaseId 
                AND motherboard_form_factor = @ChipsetSelectedMotherboard", 
                new {SelectedCaseId = selectedPccase.Id, ChipsetSelectedMotherboard = selectedMotherboard.FormFactor}) is not null)
            {
                return true;
            }

            return false; 
            }

            catch (MySqlException) { throw; }
        }
    }

    public static async Task<bool> CheckGpuCables(PowerUnit selectedPowerUnit, Gpu selectedGpu)
    {
        Dictionary<string, int> gpurequiredata = new();
        Dictionary<string, int> powerunithasdata = new();

        using (var connect = DataBaseManager.GetConnection())
        {
            try
            {
                var gpurequire = await connect.QueryAsync<AnswerTemplate>(@"SELECT connector_type AS ConnectorType, connector_count AS ConnectorCount FROM gpu_power_requirement 
                    WHERE gpu_id = @SelectedGpuId",
                new {SelectedGpuId = selectedGpu.Id});

                foreach (var obj in gpurequire)
                {
                    gpurequiredata[$"{obj.ConnectorType}"] = obj.ConnectorCount;
                }


                var powerunithas = await connect.QueryAsync<AnswerTemplate>(@"SELECT connector_type AS ConnectorType, connector_count AS ConnectorCount FROM psu_connector 
                        WHERE power_unit_id = @SelectedPowerUnitId",
                    new {SelectedPowerUnitId = selectedPowerUnit.Id});

                foreach (var obj in powerunithas)
                {
                    powerunithasdata[$"{obj.ConnectorType}"] = obj.ConnectorCount;
                }

                if (gpurequiredata.Count == 0) throw new InvalidDataException("Нет данных о разъёмах видеокарты.");
                bool result = true;

                foreach (var pair in gpurequiredata)
                {
                    if (!powerunithasdata.TryGetValue(pair.Key, out var have) || have < pair.Value)
                    {
                        result = false;
                    }
                }

                return result;
            }
            catch (MySqlException) { throw; }
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