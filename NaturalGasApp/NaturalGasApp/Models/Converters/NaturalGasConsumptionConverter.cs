using System.Text.Json;
using System.Text.Json.Serialization;

namespace NaturalGasApp.Models.Converters;

public class NaturalGasConsumptionConverter : JsonConverter<NaturalGasConsumption>
{
    public override NaturalGasConsumption? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        if (root.TryGetProperty(nameof(NaturalGasConsumption.CubicMeterConsumed), out _))
        {
            return JsonSerializer.Deserialize<NaturalGasConsumption>(root.GetRawText());
        }
        throw new JsonException("Wrong consumption type");
    }

    public override void Write(Utf8JsonWriter writer, NaturalGasConsumption value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
