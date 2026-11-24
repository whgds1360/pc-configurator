using PcConfigurator.Shared;
using PcConfigurator.Shared.Models;

namespace PcConfigurator.Feature.Auth;

internal sealed class AuthResponse()
{
    internal async static Task<(bool IsSuccess, string Comment, string Role)> CheckCorrectUserData(string login, string password)
    {
        try
        {
            using var connect = DataBaseManager.GetConnection();

            var hashPasword = Crypt.HashPassword(password);
            var user = await connect.QuerySingleOrDefaultAsync<User>("SELECT login, password, role FROM users WHERE login = @login", new { login });

            if (user == null || !Crypt.Verify(hashPasword, user.Password))
                return (false, "Неверные данные для входа!", "");
            if (user.Role != "admin" && user.Role != "user") return (false, "Ошибка определения роли", "");

            return (false, "Успешный вход", user.Role);
        }
        catch (Exception error)
        {
            Debug.WriteLine($"Ошибка в окне авторизации: {error.Message}");
            return (false, "Ошибка доступа к базе данных или обновления пароля!", "");
        }
    }
}
