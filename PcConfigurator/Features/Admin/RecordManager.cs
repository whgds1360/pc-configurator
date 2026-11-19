using PcConfigurator.Shared;

namespace PcConfigurator.Feature.Admin;

internal static class RecordManager
{
    internal static bool AddRecord(Dictionary<string, List<string>> data, string selectTable, out string comment)
    {
        if (data["Valid"].Count == 0)
        {
            comment = "Нет данных для вставки.";
            return false;
        }

        using var connect = DataBaseManager.GetConnection();
        try
        {
            var parameters = RequestManager.CreateRequestParams(data["Valid"], out var placeholders);

            connect.Execute($"INSERT INTO `{selectTable}` VALUES ({string.Join(", ", placeholders)})", parameters);

            comment = "Запись добавлена!";
            return true;
        }
        catch (Exception error)
        {
            comment = $"Ошибка записи в БД: {error.Message}";
            return false;
        }
    }

    internal static bool DeleteRecord
    {
        var data = GetValues();

        if (data["Valid"].Count > 1)
        {
            StatusBar.Content = $"Пока поддерживается только удаление по id";
            StatusBar.Foreground = new SolidColorBrush(Color.Parse("#f80000"));
        }
        else if (data["Valid"].Count == 1)
        {
            using (var connect = DataBaseManager.GetConnection())
            {
                try
                {
                    connect.Execute($"DELETE FROM `{SelectTable}` WHERE id = @id;", new { id = data["Valid"][0] });
                }
                catch (MySqlException error)
                {
                    StatusBar.Content = $"Неверные данные для удаления! {error.Message}";
                    StatusBar.Foreground = new SolidColorBrush(Color.Parse("#f80000"));
                }
            }
        }
        else
        {
            StatusBar.Content = $"Нету данных по которым можно понять, что именно удалять.";
            StatusBar.Foreground = new SolidColorBrush(Color.Parse("#f80000"));
        }
    }
}