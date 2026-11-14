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
                      (Name, CpuId, GpuId, MotherboardId, PowerUnitId, RamId, PcCaseId, CoolerId)
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