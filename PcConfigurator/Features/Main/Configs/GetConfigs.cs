using PcConfigurator.Shared;
using PcConfigurator.Shared.Models;

namespace PcConfigurator.Feature.Main.Configs;

internal sealed class getConfigs
{
    internal List<Config> GetAllConfiguration()
    {
        try
        {
            using(var connect = DataBaseManager.GetConnection())
            {
                var configs = connect.Query<Config>("SELECT * FROM `configs`").ToList();
                return configs;
            }
        }
        catch(Exception error)
        {
            Debug.WriteLine($"Ошибка получения списка конфигураций: {error.Message}");   
            return new();
        }
    }
}