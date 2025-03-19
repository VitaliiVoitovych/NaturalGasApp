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
    public ObservableCollection<ObservableNaturalGasConsumption> ObservableNaturalGasConsumptions { get; }

    public List<NaturalGasConsumption> NaturalGasConsumptions =>
        ObservableNaturalGasConsumptions.Select(c => c.Consumption).ToList();
    
    [ObservableProperty] private decimal _averageAmount;
    [ObservableProperty] private double _averageCubicMeterConsumed;
    
    public NotesService(NaturalGasDbContext dbContext, ChartsService chartsService)
    {
        ObservableNaturalGasConsumptions = [];
        _dbContext = dbContext;
        _chartsService = chartsService;
        Task.Run(LoadDataAsync);
    }

    private async Task LoadDataAsync()
    {
        var naturalGasConsumption = await _dbContext.NaturalGasConsumptions.OrderBy(n => n.Date).ToArrayAsync();
        foreach (NaturalGasConsumption consumption in naturalGasConsumption)
        {
            ObservableNaturalGasConsumptions.Add(new ObservableNaturalGasConsumption(consumption));
            ChartsService.AddValues(consumption);
        }

        UpdateAverageValues();
    }

    public async Task ImportDataAsync(IAsyncEnumerable<NaturalGasConsumption> data)
    {
        Clear();
        await foreach (var consumption in data)
        {
            AddNote(new ObservableNaturalGasConsumption(consumption));
        }
    }

    public void Clear()
    {
        var naturalGasConsumption = ObservableNaturalGasConsumptions.ToList();

        foreach (var consumption in naturalGasConsumption)
        {
            RemoveNote(consumption);
        }
    }
    
    public void AddNote(ObservableNaturalGasConsumption consumption)
    {
        DuplicateConsumptionNoteException.ThrowIfDuplicateExists(NaturalGasConsumptions, consumption.Consumption);

        var index = ObservableNaturalGasConsumptions.LastMatchingIndex(c => c.Date < consumption.Date) + 1;
        ObservableNaturalGasConsumptions.Insert(index, consumption);
        ChartsService.AddValues(index, consumption.Consumption);
        _dbContext.NaturalGasConsumptions.Add(consumption.Consumption);
        _dbContext.SaveChanges();
        UpdateAverageValues();
    }
    
    public void RemoveNote(ObservableNaturalGasConsumption consumption)
    {
        var index = ObservableNaturalGasConsumptions.IndexOf(consumption);
        ObservableNaturalGasConsumptions.RemoveAt(index);
        ChartsService.RemoveValues(index);

        _dbContext.NaturalGasConsumptions.Remove(consumption.Consumption);
        _dbContext.SaveChanges();

        UpdateAverageValues();
    }

    public void UpdateNote(ObservableNaturalGasConsumption consumption)
    {
        var index = ObservableNaturalGasConsumptions.LastMatchingIndex(c => c.Date == consumption.Date);
        
        _dbContext.NaturalGasConsumptions.Update(consumption.Consumption);
        _dbContext.SaveChanges();
        ChartsService.UpdateValues(index, consumption.Consumption);

        UpdateAverageValues();
    }
    
    private void UpdateAverageValues()
    {
        AverageAmount = ObservableNaturalGasConsumptions.Count > 0 
            ? ObservableNaturalGasConsumptions.Average(n => n.AmountToPay) 
            : 0.0m;

        AverageCubicMeterConsumed = ObservableNaturalGasConsumptions.Count > 0 
            ? ObservableNaturalGasConsumptions.Average(n => n.CubicMeterConsumed) 
            : 0.0;
    }
}