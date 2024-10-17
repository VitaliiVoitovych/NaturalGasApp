using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NaturalGasApp.Models;

public record NaturalGasConsumption(
    DateOnly Date,
    int CubicMeterConsumed,
    decimal AmountToPay)
{
    [Key, JsonIgnore]
    public int Id { get; init; }
};