namespace NaturalGasApp.Models;

public record NaturalGasConsumption(
    DateOnly Date,
    int CubicMeterConsumed,
    decimal AmountToPay)
{
    public int Id { get; init; }
};