using PcConfigurator.Shared;

namespace PcConfigurator.Feature.Auth;

internal sealed class AuthResponse()
{
    internal async static Task<(bool IsError, string Comment, string Role)> CheckCorrectUserData(string login, string password)
    {
        using (var connect = DataBaseManager.GetConnection())
        {
            try
            {
                var response = await connect.QuerySingleOrDefaultAsync<string>("SELECT role FROM users WHERE login = @login AND password = @password", new {login = login, password = password});

                if (response != null)
                {
                    switch (response)
                    {
                        case "admin": return (false, "Успешный вход", "admin");
                        case "user": return (false, "Успешный вход", "user");
                        default : return (true, "Ошибка определения роли", "");
                    }
                }

                else
                {
                    return (false, "Неверные данные для входа!", "");
                }
            }

            catch (Exception error)
            {
                Debug.WriteLine($"Ошибка в окне авторизации: {error}");
                return (true, "Ошибка входа попробуйте позже!", "");
            }
        }
    }
}