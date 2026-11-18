namespace PcConfigurator.Feature.Admin;

internal static class RequestManager
{
    internal static Dictionary<string, object> CreateRequestParams(List<string> values, out List<string> placeholders)
    {
        placeholders = new List<string>();
        var parameters = new Dictionary<string, object>();

        for (int i = 0; i < values.Count; i++)
        {
            placeholders.Add($"@p{i}");
            parameters[$"p{i}"] = values[i];
        }

        return parameters;
    }
}