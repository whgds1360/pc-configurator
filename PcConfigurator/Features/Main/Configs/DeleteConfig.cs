using PcConfigurator.Shared;

namespace PcConfigurator.Feature.Main.Configs;

internal sealed class DeleteConfigs
{
    internal static bool DeleteOneConfiguration(string name)
    {
        using(var connect = DataBaseManager.GetConnection())
        {   
            try 
            {
                if (string.IsNullOrEmpty(name)) return false;

                var affected = connect.Execute(
                    "DELETE FROM `configs` WHERE Name = @Name",
                    new { Name = name });

                if (affected == 0)
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