using MySql.Data.MySqlClient;

namespace PcConfigurator.shared.DataBase;

internal class DataBaseManager
{
    private static string? _connectionString;

    /// <summary>
    /// Инициализирует и проверяет подключение к БД.
    /// </summary>
    /// <returns>true, если подключение успешно; false, если произошла ошибка</returns>
    public static bool TryInitConnection(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) return false;

        try
        {
            using var connection = new MySqlConnection(url);
            
            connection.Open();

            _connectionString = url;
            return true;
        }
        catch (MySqlException error)
        {
            System.Diagnostics.Debug.WriteLine($"Ошибка подключения к БД: {error}");
            return false;
        }
    }

    public static string? GetConnectionUrl() => string.IsNullOrEmpty(_connectionString) ? null : _connectionString;
}