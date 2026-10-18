using System;
using System.Threading.Tasks;
using GroqSharp.Models;

namespace PcConfigurator.windows.Configurator.Helpers;

internal class AIManager
{
    private static GroqClient? _groqClient = null;

    public static void AgenInit(string apiKey, string model, float temperature, int maxtokensonresp)
    {
        _groqClient = new GroqClient(apiKey: apiKey, model: model);
    
        _groqClient.SetTemperature(temperature);
        _groqClient.SetMaxTokens(maxtokensonresp);
    }

    public static async Task<string> SendQuery(string prompt)
    {
        if (_groqClient == null)
        {
            throw new InvalidOperationException("AIManager не инициализирован. Сначала вызовите метод AgenInit.");
        }

        var response = await _groqClient.CreateChatCompletionAsync(new Message
        {
            Role = MessageRoleType.User, 
            Content = prompt ?? string.Empty
        });

        if (string.IsNullOrEmpty(response))
        {
            throw new InvalidOperationException("Прилетел пустой ответ от нейронки");
        }

        return response;
    }  
}
