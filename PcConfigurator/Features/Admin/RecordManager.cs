using PcConfigurator.Shared;
using PcConfigurator.Feature.Auth;

namespace PcConfigurator.Feature.Admin;

internal static class RecordManager
{
    internal static async Task<(bool Success, string Comment)> AddRecord(Dictionary<string, List<string>> data, string selectTable)
    {
        if (data["Valid"].Count == 0) return (false, "Нет данных для вставки.");
        try
        {
            using var connect = DataBaseManager.GetConnection();
            if (selectTable == "users") await PasswordHash.EnsureColumnAsync(connect);
            var parameters = RequestManager.CreateRequestParams(data["Valid"], out var placeholders);
            await connect.ExecuteAsync($"INSERT INTO `{selectTable}` VALUES ({string.Join(", ", placeholders)})", parameters);
            return (true, "Запись добавлена!");
        }
        catch (Exception error) { return (false, $"Ошибка записи в БД: {error.Message}"); }
    }

    internal static async Task<(bool Success, string Comment)> DeleteRecord(Dictionary<string, List<string>> data, string selectTable)
    {
        if (data["Valid"].Count != 1) return (false, "Для удаления укажите только id.");
        try
        {
            using var connect = DataBaseManager.GetConnection();
            var affected = await connect.ExecuteAsync($"DELETE FROM `{selectTable}` WHERE id = @id;", new { id = data["Valid"][0] });
            return affected == 1 ? (true, "Успешно удалено!") : (false, affected == 0 ? "Запись не найдена!" : "Удалено больше одной записи!");
        }
        catch (Exception error) { return (false, $"Ошибка удаления из БД: {error.Message}"); }
    }
}
