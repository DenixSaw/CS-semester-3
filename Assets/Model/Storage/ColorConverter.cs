using Newtonsoft.Json;
using UnityEngine;

namespace Model.Storage {
public class ColorConverter : JsonConverter {
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
        Color color = (Color)value;
        writer.WriteValue(ColorUtility.ToHtmlStringRGBA(color));
    }

    public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue,
        JsonSerializer serializer) {
        string colorString = (string)reader.Value;
        Color color;
        ColorUtility.TryParseHtmlString("#" + colorString, out color);
        return color;
    }

    public override bool CanConvert(System.Type objectType) {
        return objectType == typeof(Color);
    }
}
}