using System.IO;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PcConfigurator.shared.DataBase;

public enum OrderStatus
{
    NoConnect,
    Connect,
    ErrorConnect
}

internal class DataBaseManager
{
    public static OrderStatus currentStatus = OrderStatus.NoConnect; 

    private static string? _connectionString;

    /// <summary>
    /// Инициализирует и проверяет подключение к БД.
    /// </summary>
    /// <returns>true, если подключение успешно; false, если произошла ошибка</returns>
    public async static Task<bool> TryInitConnection(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) return false;

        try
        {
            using var connection = new MySqlConnection(url);
            
            await connection.OpenAsync();

            _connectionString = url;

            return true;
        }
        catch (MySqlException error)
        {
            System.Diagnostics.Debug.WriteLine($"Ошибка подключения к БД: {error}");
            return false;
        }
    }
    public static string CreateUrlConnection(string server, 
                                                string port, 
                                                string db, 
                                                string user, 
                                                string password)
    {
        return $"Server={server};Port={port};Database={db};Uid={user};Pwd={password};";
    }

    public static void ChangeStatus(OrderStatus newStatus)
    {
        currentStatus = newStatus;
    }

    public MySqlConnection GetConnection() => !string.IsNullOrEmpty(_connectionString) ? new MySqlConnection() : throw new InvalidDataException("Ошибка БД");

    public static OrderStatus GetStatus() => currentStatus;

    public static string GetConnectionUrl() => string.IsNullOrEmpty(_connectionString) ? "" : _connectionString;
}