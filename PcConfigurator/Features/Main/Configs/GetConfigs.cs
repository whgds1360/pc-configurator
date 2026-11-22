using PcConfigurator.Shared;
using PcConfigurator.Shared.Models;

namespace PcConfigurator.Feature.Main.Configs;

internal sealed class GetConfigs
{
    internal static List<Config> GetAllConfiguration()
    {
        try
        {
            using(var connect = DataBaseManager.GetConnection())
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                var configs = connect.Query<Config>("SELECT * FROM `configs` WHERE user_login = @UserLogin", new { UserLogin = DataBaseManager.CurrentUser }).ToList();
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