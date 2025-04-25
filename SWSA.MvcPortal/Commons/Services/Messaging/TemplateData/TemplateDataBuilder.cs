namespace SWSA.MvcPortal.Commons.Services.Messaging.TemplateData;

public static class TemplateDataBuilder
{
    public static Dictionary<string, string> From<T>(T model) where T : class
    {
        return typeof(T).GetProperties()
            .ToDictionary(p => ToCamel(p.Name), p => p.GetValue(model)?.ToString() ?? string.Empty);

        string ToCamel(string name) =>
            char.ToLowerInvariant(name[0]) + name.Substring(1);
    }
}
