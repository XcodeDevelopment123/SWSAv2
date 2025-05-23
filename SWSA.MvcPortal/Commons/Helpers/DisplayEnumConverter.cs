using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SWSA.MvcPortal.Commons.Helpers;

public class DisplayEnumConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType.IsEnum;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var enumType = value.GetType();
        var enumName = Enum.GetName(enumType, value);
        var displayAttr = enumType
            .GetField(enumName)
            ?.GetCustomAttribute<DisplayAttribute>();

        writer.WriteValue(displayAttr?.Name ?? enumName);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var str = reader.Value?.ToString();
        foreach (var field in objectType.GetFields())
        {
            var displayAttr = field.GetCustomAttribute<DisplayAttribute>();
            if ((displayAttr != null && displayAttr.Name == str) || field.Name == str)
            {
                return Enum.Parse(objectType, field.Name);
            }
        }
        throw new JsonSerializationException($"Unknown value: {str}");
    }
}