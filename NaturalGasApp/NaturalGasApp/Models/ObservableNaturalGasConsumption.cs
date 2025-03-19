namespace NaturalGasApp.Models;

public class ObservableNaturalGasConsumption(NaturalGasConsumption consumption) : ObservableObject
{
    public NaturalGasConsumption Consumption { get; } = consumption;

    public DateOnly Date
    {
        get => Consumption.Date;
        set => SetProperty(Consumption.Date, value, Consumption, 
            (consumption, date) => consumption.Date = date);
    }
    
    public decimal AmountToPay
    {
        get => Consumption.AmountToPay;
        set => SetProperty(Consumption.AmountToPay, value, Consumption,
            (consumption, amount) => consumption.AmountToPay = amount);
    }

    public double CubicMeterConsumed
    {
        get => Consumption.CubicMeterConsumed;
        set => SetProperty(Consumption.CubicMeterConsumed, value, Consumption,
            (consumption, cubicMeterConsumed) => consumption.CubicMeterConsumed = cubicMeterConsumed);
    }
}