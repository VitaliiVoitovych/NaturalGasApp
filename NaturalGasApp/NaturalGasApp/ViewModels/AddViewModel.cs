using NaturalGasApp.Exceptions;

namespace NaturalGasApp.ViewModels;

public partial class AddViewModel(NotesService notesService) : ObservableObject
{
    private static readonly Dictionary<string, int> _months = new()
    {
        { "Січень", 1}, { "Лютий" , 2}, { "Березень" , 3}, { "Квітень" , 4},
        { "Травень", 5}, { "Червень" , 6}, { "Липень" , 7}, { "Серпень" , 8},
        { "Вересень", 9}, { "Жовтень" , 10}, { "Листопад" , 11}, { "Грудень" , 12},
    };

    public List<string> Months => [.. _months.Keys];

    public List<int> Years => [.. Enumerable.Range(DateTime.Now.Year - 5, 6)];
    
    [ObservableProperty] private string _selectedMonth = _months.First(m => m.Value == DateTime.Now.Month).Key;
    [ObservableProperty] private int _selectedYear = DateTime.Now.Year;
    [ObservableProperty] private double _cubicMeterConsumed;
    [ObservableProperty] private decimal _cubicMeterPrice = 7.95689m;

    [RelayCommand]
    private async Task Add()
    {
        var amountToPay = Convert.ToDecimal(CubicMeterConsumed) * CubicMeterPrice;

        var consumption = new NaturalGasConsumption(new DateOnly(SelectedYear, _months[SelectedMonth], DateTime.Now.Day),
            CubicMeterConsumed, amountToPay);
        try
        {
            InvalidConsumptionDataException.ThrowIfDateInvalid(consumption);
            notesService.AddNote(consumption);
            ChangeMonthAndYear();
        }
        catch (DuplicateConsumptionNoteException)
        {
            await Shell.Current.DisplayAlert("Помилка!", "Запис про цей місяць вже є", "Зрозуміло");
        }
        catch (InvalidConsumptionDataException)
        {
            await Shell.Current.DisplayAlert("Помилка!", "Не можна додавати запис \r\nпро поточний чи майбутній місяць", "Зрозуміло");
        }
    }

    private void ChangeMonthAndYear()
    {
        var monthNumber = _months[SelectedMonth];
        if (monthNumber == 12)
        {
            SelectedMonth = _months.First().Key;
            SelectedYear++;
        }
        else
        {
            SelectedMonth = _months.First(m => m.Value == monthNumber + 1).Key;
        }
    }
}