using Dapper;
using Org.BouncyCastle.Asn1.Ocsp;
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
            if (connect.QueryFirstOrDefault(@"SELECT Chipset FROM cpucompatibility 
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
/*
    public static bool CheckFormFactor(Motherboard selectedMotherboard, PcCase selectedPccase)
    {
        using (var connect = DataBaseManager.GetConnection())
        {
            if (selectedPccase.FormFactor == selectedMotherboard.FormFactor)
            {
                return true;
            }

            return false;
        }
    }

    public static bool CheckGpuCables()
    {}

    public static bool CheckDdrType()
    {}
*/



}