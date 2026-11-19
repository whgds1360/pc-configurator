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

    internal static bool DeleteRecord(Dictionary<string, List<string>> data, string selectTable,  out string comment)
    {
        if (data["Valid"].Count > 1)
        {
            comment = $"Пока поддерживается только удаление по id";
            return false;
        }
        else if (data["Valid"].Count == 1)
        {
            using (var connect = DataBaseManager.GetConnection())
            {
                try
                {
                    connect.Execute($"DELETE FROM `{selectTable}` WHERE id = @id;", new { id = data["Valid"][0] });

                    comment = "Успешно удалено!";
                    return true;
                }
                catch (MySqlException error)
                {
                    comment = $"Неверные данные для удаления! {error.Message}";
                    return false;
                }
            }
        }
        else
        {
            comment = $"Нету данных по которым можно понять, что именно удалять.";
            return false;
        }
    }
}