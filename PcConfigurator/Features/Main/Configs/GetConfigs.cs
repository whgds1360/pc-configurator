using PcConfigurator.Shared;
using PcConfigurator.Shared.Models;

namespace PcConfigurator.Feature.Main.Configs;

internal sealed class GetConfigs
{
    internal static async Task<List<Config>> GetAllConfiguration()
    {
        using var connect = DataBaseManager.GetConnection();
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        return (await connect.QueryAsync<Config>("SELECT * FROM `configs` WHERE user_login = @UserLogin", new { UserLogin = DataBaseManager.CurrentUser })).ToList();
    }
}
