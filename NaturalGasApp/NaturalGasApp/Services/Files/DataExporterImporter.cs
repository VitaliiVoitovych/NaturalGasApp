using NaturalGasApp.Models.Converters;
using System.Text.Json;

namespace NaturalGasApp.Services.Files;

public class DataExporterImporter
{
    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        Converters = { new NaturalGasConsumptionConverter() }
    };
    public static async Task<IAsyncEnumerable<NaturalGasConsumption>> ImportAsync(Stream stream)
    {
        return (await JsonSerializer.DeserializeAsync<IAsyncEnumerable<NaturalGasConsumption>>(stream, Options))!;
    }
    public static async Task ExportAsync<T>(string path, IEnumerable<T> collection)
        where T : NaturalGasConsumption
    {
        await using var stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        await JsonSerializer.SerializeAsync(stream, collection);
    }
}
