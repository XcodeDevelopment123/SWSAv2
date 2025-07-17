using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SWSA.MvcPortal.Commons.Helpers;

public class DisplayEnumConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        var type = Nullable.GetUnderlyingType(objectType) ?? objectType;
        return type.IsEnum;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        var enumType = value.GetType();
        var enumName = Enum.GetName(enumType, value);

        if (enumName == null)
        {
            writer.WriteValue(value.ToString());
            return;
        }

        var displayAttr = enumType
            .GetField(enumName)
            ?.GetCustomAttribute<DisplayAttribute>();

        writer.WriteValue(displayAttr?.Name ?? enumName);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            if (Nullable.GetUnderlyingType(objectType) != null)
                return null;
            throw new JsonSerializationException("Cannot convert null to non-nullable enum.");
        }

        var str = reader.Value?.ToString();
        var enumType = Nullable.GetUnderlyingType(objectType) ?? objectType;

        foreach (var field in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var displayAttr = field.GetCustomAttribute<DisplayAttribute>();
            if ((displayAttr != null && displayAttr.Name == str) || field.Name == str)
            {
                return Enum.Parse(enumType, field.Name);
            }
        }

        throw new JsonSerializationException($"Unknown value: {str}");
    }
}