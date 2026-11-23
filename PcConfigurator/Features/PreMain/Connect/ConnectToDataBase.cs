using PcConfigurator.Shared;
using PcConfigurator.Feature.Auth;

namespace PcConfigurator.Feature.PreMain.Connect;

internal sealed class ConnectToDatabaseHandler
{
    internal static async Task<bool> ConnectToDataBase(string server, string port, string db, string user, string password)
    {   
        if (DataBaseManager.GetStatus()!= DbConnectionStatus.Connected)
        {
            var connectUrl = DataBaseManager.CreateUrlConnection(server: server, 
                                                                    port: port, 
                                                                    db: db, 
                                                                    user: user, 
                                                                    password: password);

            if (await DataBaseManager.TryInitConnection(url: connectUrl))
            {   
                DataBaseManager.ChangeStatus(newStatus: DbConnectionStatus.Connected);

                try
                {
                    using var context = new ApplicationContext();
                    await context.Database.EnsureCreatedAsync();
                    await PasswordHash.MigrateLegacyAsync();
                    return true;
                }
                catch (Exception error)
                {
                    Debug.WriteLine($"Ошибка подготовки базы данных: {error.Message}");
                    DataBaseManager.ChangeStatus(DbConnectionStatus.Error);
                    return false;
                }
    
            }
            else
            {
                DataBaseManager.ChangeStatus(newStatus:DbConnectionStatus.Error);

                return false;
            }
        }

        return false;
    }
}