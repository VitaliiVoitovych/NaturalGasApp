using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using NaturalGasApp.Services.Charting.Styles;

namespace NaturalGasApp.Services.Charting;

public class ChartsService
{
    private readonly ObservableCollection<decimal> _amountsToPayValues = [];
    private readonly ObservableCollection<int> _cubicMeterConsumed = [];
    private readonly ObservableCollection<string> _dateLabels = [];

    public void AddValues(NaturalGasConsumption consumption)
    {
        _dateLabels.Add(consumption.Date.ToString(ChartConstants.DateFormat));
        _cubicMeterConsumed.Add(consumption.CubicMeterConsumed);
        _amountsToPayValues.Add(consumption.AmountToPay);
    }

    public void AddValues(int index, NaturalGasConsumption consumption)
    {
        _dateLabels.Insert(index, consumption.Date.ToString(ChartConstants.DateFormat));
        _cubicMeterConsumed.Insert(index, consumption.CubicMeterConsumed);
        _amountsToPayValues.Insert(index, consumption.AmountToPay);
    }

    public void RemoveValues(int index)
    {
        _dateLabels.RemoveAt(index);
        _cubicMeterConsumed.RemoveAt(index);
        _amountsToPayValues.RemoveAt(index);
    }

    public IEnumerable<ISeries> AmountsToPaySeries =>
    [
        ChartUtils.CreateLineSeries(_amountsToPayValues, ChartColors.AmountToPaySeriesColor),
    ];

    public IEnumerable<Axis> AmountToPayYAxes =>
    [
        ChartUtils.CreateValueYAxis(d => d.ToString("f2")),
    ];

    public IEnumerable<ISeries> CubicMeterConsumedSeries =>
    [
        ChartUtils.CreateLineSeries(_cubicMeterConsumed, ChartColors.GasSeriesColor, "Спожито м³")
    ];

    public IEnumerable<Axis> CubicMeterConsumedYAxes =>
    [
        ChartUtils.CreateValueYAxis(),
    ];

    public IEnumerable<Axis> DateXAxes =>
    [
        new Axis
        {
            Labels = _dateLabels,
            MaxLimit = ChartConstants.MaxXAxisLabels,
            LabelsPaint = ChartPaints.AxisLabelsPaint,
        }
    ];

    public SolidColorPaint LegendTextPaint => ChartPaints.LegendTextPaint;
}