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
}