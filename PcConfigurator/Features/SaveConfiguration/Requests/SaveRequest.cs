using PcConfigurator.Shared;

namespace PcConfigurator.Feature.SaveConfiguration.Requests;

internal sealed class SaveRequest
{
    internal static bool AcceptConfiguration(string configName,
                                        int cpuId,
                                        int gpuId,
                                        int motherboardId,
                                        int powerUnitId,
                                        int ramId,
                                        int pcCaseId,
                                        int coolerId)
    {

        try
        {
            using var connect = DataBaseManager.GetConnection();
            var affected = connect.Execute(
                @"INSERT INTO configs
                      (user_login, name, cpu_id, gpu_id, motherboard_id, power_unit_id, ram_id, pc_case_Id, cooler_id)
                  VALUES
                      (@UserLogin, @Name, @CpuId, @GpuId, @MotherboardId, @PowerUnitId, @RamId, @PcCaseId, @CoolerId)",
                new
                {
                    UserLogin = DataBaseManager.CurrentUser,
                    Name = configName,
                    CpuId         = cpuId,
                    GpuId         = gpuId,
                    MotherboardId = motherboardId,
                    PowerUnitId   = powerUnitId,
                    RamId         = ramId,
                    PcCaseId      = pcCaseId,
                    CoolerId      = coolerId
                });

            switch(affected)
                    {
                        case 0: 
                            return false;

                        case 1: 
                            return true;

                        case >1: 
                            return false;

                        default: 
                            return false;
                    }
        }

        catch (Exception error)
        {
            Debug.WriteLine($"Ошибка при сохранении конфига: {error.Message}");
            return false;
        }
    }
}