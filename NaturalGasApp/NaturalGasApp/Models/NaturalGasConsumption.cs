using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NaturalGasApp.Models;

public class NaturalGasConsumption(
    DateOnly date,
    double cubicMeterConsumed,
    decimal amountToPay)
{
    [Key, JsonIgnore]
    public int Id { get; init; }

    public DateOnly Date { get; set; } = date;
    public double CubicMeterConsumed { get; set; } = cubicMeterConsumed;
    public decimal AmountToPay { get; set; } = amountToPay;
};