using System.Security.Cryptography;
using System.Text;
using PcConfigurator.Shared;

namespace PcConfigurator.Feature.Auth;

internal static class PasswordHash
{
    private const int Iterations = 600000;
    internal static string Create(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, 32);
        return $"$pbkdf2-sha256${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
    }

    internal static bool Verify(string password, string stored)
    {
        if (!stored.StartsWith("$pbkdf2-sha256$", StringComparison.Ordinal))
            return CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(stored));
        try
        {
            var parts = stored.Split('$');
            if (parts.Length != 5 || !int.TryParse(parts[2], out var iterations) || iterations < 1 || iterations > 2000000) return false;
            var salt = Convert.FromBase64String(parts[3]);
            var expected = Convert.FromBase64String(parts[4]);
            if (salt.Length != 16 || expected.Length != 32) return false;
            return CryptographicOperations.FixedTimeEquals(expected, Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, expected.Length));
        }
        catch (FormatException) { return false; }
    }

    internal static async Task MigrateLegacyAsync()
    {
        using var connection = DataBaseManager.GetConnection();
        await EnsureColumnAsync(connection);
        var users = await connection.QueryAsync<PcConfigurator.Shared.Models.User>("SELECT login, password FROM users");
        foreach (var user in users)
        {
            if (user.Password.StartsWith("$pbkdf2-sha256$", StringComparison.Ordinal)) continue;
            var hash = await Task.Run(() => Create(user.Password));
            if (await connection.ExecuteAsync("UPDATE users SET password = @hash WHERE login = @login AND BINARY password = BINARY @old", new { hash, login = user.Login, old = user.Password }) != 1)
                throw new InvalidOperationException("Пароль изменился во время обновления. Повторите подключение.");
        }
    }

    internal static async Task EnsureColumnAsync(MySqlConnection connection)
    {
        var length = await connection.QuerySingleAsync<long>("SELECT CHARACTER_MAXIMUM_LENGTH FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'users' AND COLUMN_NAME = 'password'");
        if (length < 255) await connection.ExecuteAsync("ALTER TABLE users MODIFY COLUMN password VARCHAR(255) NOT NULL");
    }
}
