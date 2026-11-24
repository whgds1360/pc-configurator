using PcConfigurator.Shared;

namespace PcConfigurator.Feature.Main.Configs;

internal sealed class DeleteConfigs
{
    internal static async Task<bool> DeleteOneConfiguration(int id)
    {
        using(var connect = DataBaseManager.GetConnection())
        {   
            try 
            {
                if (id <= 0) return false;

                var affected = await connect.ExecuteAsync(
                    "DELETE FROM `configs` WHERE id = @Id AND user_login = @UserLogin",
                    new { Id = id, UserLogin = DataBaseManager.CurrentUser });

                if (affected != 1)
                {
                    Debug.WriteLine("Запись не найдена");
                    return false;
                }

                return true;
            }
            catch (Exception error)
            {
                Debug.WriteLine($"Ошибка удаления: {error.Message}");
                return false;
            }
        }
    }
}