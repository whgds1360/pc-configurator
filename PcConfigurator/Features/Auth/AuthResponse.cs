using PcConfigurator.Shared;
using PcConfigurator.Shared.Models;

namespace PcConfigurator.Feature.Auth;

internal sealed class AuthResponse()
{
    internal async static Task<(bool IsError, string Comment, string Role)> CheckCorrectUserData(string login, string password)
    {
        try
        {
            using var connect = DataBaseManager.GetConnection();
            var user = await connect.QuerySingleOrDefaultAsync<User>("SELECT login, password, role FROM users WHERE login = @login", new { login });
            if (user == null || !await Task.Run(() => PasswordHash.Verify(password, user.Password)))
                return (true, "Неверные данные для входа!", "");
            if (user.Role != "admin" && user.Role != "user") return (true, "Ошибка определения роли", "");
            if (!user.Password.StartsWith("$pbkdf2-sha256$", StringComparison.Ordinal))
            {
                await PasswordHash.EnsureColumnAsync(connect);
                var hash = await Task.Run(() => PasswordHash.Create(password));
                if (await connect.ExecuteAsync("UPDATE users SET password = @hash WHERE login = @login AND BINARY password = BINARY @old", new { hash, login, old = user.Password }) != 1)
                    return (true, "Пароль изменился, повторите вход.", "");
            }
            return (false, "Успешный вход", user.Role);
        }
        catch (Exception error)
        {
            Debug.WriteLine($"Ошибка в окне авторизации: {error}");
            return (true, "Ошибка доступа к базе данных или обновления пароля!", "");
        }
    }
}
