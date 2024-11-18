using Microsoft.EntityFrameworkCore;
using NaturalGasApp.EfStructures;
using NaturalGasApp.Exceptions;
using NaturalGasApp.Extensions;
using NaturalGasApp.Services.Charting;

namespace NaturalGasApp.Services.Notes;

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
        foreach (NaturalGasConsumption consumption in naturalGasConsumption)
        {
            NaturalGasConsumptions.Add(consumption);
            ChartsService.AddValues(consumption);
        }

        UpdateAverageValues();
    }

    public async Task ImportDataAsync(IAsyncEnumerable<NaturalGasConsumption> data)
    {
        Clear();
        await foreach (var consumption in data)
        {
            AddNote(consumption);
        }
    }

    public void Clear()
    {
        var naturalGasConsumption = NaturalGasConsumptions.ToList();

        foreach (var consumption in naturalGasConsumption)
        {
            RemoveNote(consumption);
        }
    }
    
    public void AddNote(NaturalGasConsumption consumption)
    {
        DuplicateConsumptionNoteException.ThrowIfDuplicateExists(NaturalGasConsumptions, consumption);

        var index = NaturalGasConsumptions.LastMatchingIndex(c => c.Date < consumption.Date) + 1;
        NaturalGasConsumptions.Insert(index, consumption);
        ChartsService.AddValues(index, consumption);
        _dbContext.NaturalGasConsumptions.Add(consumption);
        _dbContext.SaveChanges();
        UpdateAverageValues();
    }
    
    public void RemoveNote(NaturalGasConsumption consumption)
    {
        var index = NaturalGasConsumptions.IndexOf(consumption);
        NaturalGasConsumptions.RemoveAt(index);
        ChartsService.RemoveValues(index);

        _dbContext.NaturalGasConsumptions.Remove(consumption);
        _dbContext.SaveChanges();

        UpdateAverageValues();
    }

    private void UpdateAverageValues()
    {
        AverageAmount = NaturalGasConsumptions.Count > 0 
            ? NaturalGasConsumptions.Average(n => n.AmountToPay) 
            : 0.0m;

        AverageCubicMeterConsumed = NaturalGasConsumptions.Count > 0 
            ? NaturalGasConsumptions.Average(n => n.CubicMeterConsumed) 
            : 0.0;
    }
}