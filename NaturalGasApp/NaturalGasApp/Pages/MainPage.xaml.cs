namespace NaturalGasApp.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        AmountToPayChart.UpdateChart();
        CubicMeterConsumedChart.UpdateChart();
    }
}