using PcConfigurator.Shared;

namespace PcConfigurator.Feature.PreMain.Connect;

internal sealed class ConnectToDatabaseHandler
{
    internal async Task<bool> ConnectToDataBase(string server, string port, string db, string user, string password)
    {   
        if (DataBaseManager.GetStatus()== DbConnectionStatus.Connected)
        {
            var connectUrl = DataBaseManager.CreateUrlConnection(server: server, 
                                                                    port: port, 
                                                                    db: db, 
                                                                    user: user, 
                                                                    password: password);

            if (await DataBaseManager.TryInitConnection(url: connectUrl))
            {   
                DataBaseManager.ChangeStatus(newStatus: DbConnectionStatus.Connected);

                using (var context = new ApplicationContext())
                {
                    await context.Database.EnsureCreatedAsync();
                };

                return true;
    
            }
            else
            {
                DataBaseManager.ChangeStatus(newStatus:DbConnectionStatus.Error);

                return false;
            }
        }

        return false
    }
}