namespace PcConfigurator.Shared;

internal class DataBaseManager
{
    private static Dictionary<string, string> _dataBaseInfo = new();
    private static DbConnectionStatus _currentStatus = DbConnectionStatus.Disconected; 
    private static string? _connectionString = string.Empty;

    /// <summary>
    /// Инициализирует и проверяет подключение к БД.
    /// </summary>
    /// <returns>true, если подключение успешно; false, если произошла ошибка</returns>
    internal async static Task<bool> TryInitConnection(string url)
    {
        if (string.IsNullOrEmpty(url)) return false;

        try
        {
            using (var connection = new MySqlConnection(url))
            {
                await connection.OpenAsync();

                ChangeStatus(DbConnectionStatus.Connected);
                _connectionString = url;
            }

            return true;
        }

        catch (MySqlException error)
        {
            ChangeStatus(DbConnectionStatus.Error);
            Debug.WriteLine($"Ошибка подключения к БД: {error.Message}");

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
            ChangeStatus(DbConnectionStatus.Error);
            Debug.WriteLine("Неверные данные или их формат для создания ссылки подключения БД");
        }

        return "";
    }

    internal static void ChangeStatus(DbConnectionStatus newStatus){_currentStatus = newStatus;}

    internal static Dictionary<string, string> GetInfo() => _dataBaseInfo;

    internal static MySqlConnection GetConnection() => !string.IsNullOrEmpty(_connectionString) ? new MySqlConnection(_connectionString) : throw new InvalidDataException("Ошибка БД");

    internal static DbConnectionStatus GetStatus() => _currentStatus;

    internal static string GetConnectionUrl() => string.IsNullOrEmpty(_connectionString) ? "" : _connectionString;
}