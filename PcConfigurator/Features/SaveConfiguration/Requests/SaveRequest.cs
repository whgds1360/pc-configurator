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
            var newId = connect.Execute(
                @"INSERT INTO configs
                      (name, cpu_id, gpu_id, motherboard_id, power_unit_id, ram_id, pc_case_Id, cooler_id)
                  VALUES
                      (@Name, @CpuId, @GpuId, @MotherboardId, @PowerUnitId, @RamId, @PcCaseId, @CoolerId)",
                new
                {
                    Name = configName,
                    CpuId         = cpuId,
                    GpuId         = gpuId,
                    MotherboardId = motherboardId,
                    PowerUnitId   = powerUnitId,
                    RamId         = ramId,
                    PcCaseId      = pcCaseId,
                    CoolerId      = coolerId
                });

            return true;
        }

        catch (Exception error)
        {
            Debug.WriteLine($"Ошибка при сохранении конфига: {error.Message}");
            return false;
        }
    }
}