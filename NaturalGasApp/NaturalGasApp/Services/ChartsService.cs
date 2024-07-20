using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;

namespace NaturalGasApp.Services;

public class ChartsService
{
    private readonly ObservableCollection<decimal> _amountsToPayValues = [];
    private readonly ObservableCollection<int> _cubicMeterConsumed = [];
    private readonly ObservableCollection<string> _dateLabels = [];

    public async Task UpdateValues(ObservableCollection<NaturalGasConsumption> naturalGasConsumptions)
    {
        await Task.Run(() =>
        {
            _amountsToPayValues.Clear();
            _cubicMeterConsumed.Clear();
            _dateLabels.Clear();

            foreach (var r in naturalGasConsumptions)
            {
                AddValues(r);
            }
        });
    }

    private void AddValues(NaturalGasConsumption record)
    {
        _dateLabels.Add(record.Date.ToString("MMM yyyy"));
        _cubicMeterConsumed.Add(record.CubicMeterConsumed);
        _amountsToPayValues.Add(record.AmountToPay);
    }
    
    public IEnumerable<ISeries> AmountsToPaySeries =>
    [
        new LineSeries<decimal>
        {
            Name = "Оплата",
            Values = _amountsToPayValues,
            Fill = null,
            Stroke = new SolidColorPaint(SKColor.Parse("#256F33")) { StrokeThickness = 3},
            GeometryFill = new SolidColorPaint(SKColor.Parse("#256F33")),
            GeometryStroke = new SolidColorPaint(SKColor.Parse("#256F33")) { StrokeThickness = 2},
            GeometrySize = 5,
        }
    ];

    public IEnumerable<Axis> AmountToPayYAxes =>
    [
        new Axis
        {
            SeparatorsPaint = new SolidColorPaint(SKColors.LightGray) 
            { 
                StrokeThickness = 0.5f, 
                PathEffect = new DashEffect([4f,4f])
            },
            Labeler = d => d.ToString("f2"),
            TextSize = 15,
            LabelsPaint = new SolidColorPaint(SKColor.Parse("#95b5cf"))
        }
    ];
    
    public IEnumerable<ISeries> CubicMeterConsumedSeries =>
    [
        new LineSeries<int>
        {
            Name = "Спожито",
            Values = _cubicMeterConsumed,
            Fill = null,
            Stroke = new SolidColorPaint(SKColor.Parse("#367fd7")) { StrokeThickness = 3},
            GeometryFill = new SolidColorPaint(SKColor.Parse("#367fd7")),
            GeometryStroke = new SolidColorPaint(SKColor.Parse("#367fd7")) { StrokeThickness = 3},
            GeometrySize = 5
        }
    ];
    
    public IEnumerable<Axis> CubicMeterConsumedYAxes => 
    [
        new Axis
        {
            SeparatorsPaint = new SolidColorPaint(SKColors.LightGray)
            {
                StrokeThickness = 0.5f,
                PathEffect = new DashEffect([4f,4f])
            },
            LabelsPaint = new SolidColorPaint(SKColor.Parse("#95b5cf")),
        }
    ];
    
    public IEnumerable<Axis> DateXAxes =>
    [
        new Axis
        {
            Labels = _dateLabels,
            MaxLimit = 12,
            LabelsPaint = new SolidColorPaint(SKColor.Parse("#95b5cf")),
        }
    ];

    public SolidColorPaint LegendTextPaint => new SolidColorPaint()
    {
        Color = SKColor.Parse("#abb0b3"),
    };
}