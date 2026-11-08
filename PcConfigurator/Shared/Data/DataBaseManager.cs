using System.Diagnostics;

namespace PcConfigurator.Shared;

internal enum DbConnectStatus
{
    NoConnect,
    Connect,
    ErrorConnect
}

internal class DataBaseManager
{
    private static Dictionary<string, string> _dataBaseInfo = new();
    private static DbConnectStatus _currentStatus = DbConnectStatus.NoConnect; 
    private static string? _connectionString = "Server=localhost;Port=3306;Database=test;Uid=root;Pwd=;";

    /// <summary>
    /// Инициализирует и проверяет подключение к БД.
    /// </summary>
    /// <returns>true, если подключение успешно; false, если произошла ошибка</returns>
    internal async static Task<bool> TryInitConnection(string url)
    {
        url = url.Trim();

        if (string.IsNullOrEmpty(url)) return false;

        try
        {
            using (var connection = new MySqlConnection(url))
            {
                await connection.OpenAsync();
                _connectionString = url;
            }

            return true;
        }

        catch (MySqlException error)
        {
            Debug.WriteLine($"Ошибка подключения к БД: {error}");

            return false;
        }
    }
    internal static string CreateUrlConnection(string server, 
                                                string port, 
                                                string db, 
                                                string user, 
                                                string password)
    {
        if (uint.TryParse(port, out uint readyPort))
        {
            _dataBaseInfo["ip"] = server;
            _dataBaseInfo["port"] = port;
            _dataBaseInfo["db"] = db;
            _dataBaseInfo["user"] = user;

            var url = new MySqlConnectionStringBuilder()
            {
                Server = server,
                Port = readyPort,
                Database = db,
                UserID = user,
                Password = password,
                
            };

            return url.ConnectionString;
        }

        else
        {
            Debug.WriteLine("Неверные данные или их формат для создания ссылки подключения БД");
        }

        return "";
    }

    internal static void ChangeStatus(DbConnectStatus newStatus)
    {
        _currentStatus = newStatus;
    }

    internal static Dictionary<string, string> GetInfo() => _dataBaseInfo;

    internal static MySqlConnection GetConnection() => !string.IsNullOrEmpty(_connectionString) ? new MySqlConnection(_connectionString) : throw new InvalidDataException("Ошибка БД");

    internal static DbConnectStatus GetStatus() => _currentStatus;

    internal static string GetConnectionUrl() => string.IsNullOrEmpty(_connectionString) ? "" : _connectionString;
}