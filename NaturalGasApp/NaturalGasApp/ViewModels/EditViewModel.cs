namespace NaturalGasApp.ViewModels;

public partial class EditViewModel(NotesService notesService) : ObservableObject, IQueryAttributable
{
    public ObservableNaturalGasConsumption Consumption { get; private set; }
    
    [ObservableProperty] private double _cubicMeterConsumed;
    [ObservableProperty] private decimal _cubicMeterPrice = 7.95689m;

    [RelayCommand]
    private void GoToBack() => Shell.Current.GoToAsync("..", true);

    [RelayCommand]
    private void Update()
    {
        var amountToPay = Convert.ToDecimal(CubicMeterConsumed) * CubicMeterPrice;

        Consumption.CubicMeterConsumed = CubicMeterConsumed;
        Consumption.AmountToPay = amountToPay;
        
        notesService.UpdateNote(Consumption);
    }
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Consumption = (ObservableNaturalGasConsumption)query[nameof(Consumption)];
        OnPropertyChanged(nameof(Consumption));

        CubicMeterConsumed = Consumption.CubicMeterConsumed;
    }
}