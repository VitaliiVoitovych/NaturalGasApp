using Microsoft.EntityFrameworkCore;
using NaturalGasApp.EfStructures;

namespace NaturalGasApp.Services;

public partial class NotesService : ObservableObject
{
    private readonly NaturalGasDbContext _dbContext;
    private readonly ChartsService _chartsService;

    public ChartsService ChartsService => _chartsService;
    public ObservableCollection<NaturalGasConsumption> NaturalGasConsumptions { get; }

    [ObservableProperty] private decimal _averageAmount;
    [ObservableProperty] private double _averageCubicMeterConsumed;
    
    public NotesService(NaturalGasDbContext dbContext, ChartsService chartsService)
    {
        NaturalGasConsumptions = [];
        _dbContext = dbContext;
        _chartsService = chartsService;
        Task.Run(LoadDataAsync);
    }

    private async Task LoadDataAsync()
    {
        var naturalGasConsumption = await _dbContext.NaturalGasConsumptions.OrderBy(n => n.Date).ToArrayAsync();
        foreach (var r in naturalGasConsumption)
        {
            NaturalGasConsumptions.Add(r);
        }

        UpdateAverageValues();
        await ChartsService.UpdateValues(NaturalGasConsumptions);
    }

    public async Task ClearAsync()
    {
        var naturalGasConsumption = NaturalGasConsumptions.ToList();

        foreach (var record in naturalGasConsumption)
        {
            await RemoveNoteAsync(record);
        }
    }
    
    public async Task AddNoteAsync(NaturalGasConsumption record)
    {
        if ( NaturalGasConsumptions.Any(r => EqualsYearAndMonth(r.Date, record.Date)))
            throw new ArgumentException("Запис про цей місяць вже є");

        NaturalGasConsumptions.Add(record);
        _dbContext.NaturalGasConsumptions.Add(record);
        await _dbContext.SaveChangesAsync();

        SortElectricityConsumptions();
        UpdateAverageValues();
        await ChartsService.UpdateValues(NaturalGasConsumptions);
    }
    
    public async Task RemoveNoteAsync(NaturalGasConsumption record)
    {
        NaturalGasConsumptions.Remove(record);
        _dbContext.NaturalGasConsumptions.Remove(record);
        await _dbContext.SaveChangesAsync();

        UpdateAverageValues();
        await ChartsService.UpdateValues(NaturalGasConsumptions);
    }

    private void SortElectricityConsumptions()
    {
        var sortedList = NaturalGasConsumptions.OrderBy(e => e.Date).ToList();
        NaturalGasConsumptions.Clear();
        foreach (var r in sortedList)
        {
            NaturalGasConsumptions.Add(r);
        }
    }

    private bool EqualsYearAndMonth(DateOnly date1, DateOnly date2)
    {
        return (date1.Year, date1.Month) == (date2.Year, date2.Month);
    }

    private void UpdateAverageValues()
    {
        if (NaturalGasConsumptions.Count > 0)
        {
            AverageAmount = NaturalGasConsumptions.Average(e => e.AmountToPay);
            AverageCubicMeterConsumed = NaturalGasConsumptions.Average(e => e.CubicMeterConsumed);
        }
        else
        {
            AverageAmount = 0.0m;
            AverageCubicMeterConsumed = 0.0;
        }
    }
}