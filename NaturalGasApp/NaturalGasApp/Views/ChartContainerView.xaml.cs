using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Drawing;
using ChartPaints = NaturalGasApp.Services.Charting.Styles.ChartPaints;

namespace NaturalGasApp.Views;

public partial class ChartContainerView : ContentView
{
    public static readonly BindableProperty TextProperty =
		BindableProperty.Create(nameof(Text), typeof(string), typeof(ChartContainerView), default);

    public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}

    public static readonly BindableProperty AverageValueProperty =
        BindableProperty.Create(nameof(AverageValue), typeof(string), typeof(ChartContainerView), default);

    public string AverageValue
    {
        get => (string)GetValue(AverageValueProperty);
        set => SetValue(AverageValueProperty, value);
    }

    public static readonly BindableProperty SeriesProperty =
        BindableProperty.Create(nameof(Series), typeof(IEnumerable<ISeries>), typeof(ChartContainerView), default);

    public IEnumerable<ISeries> Series
	{
		get => (IEnumerable<ISeries>)GetValue(SeriesProperty);
        set => SetValue(SeriesProperty, value);
	}

    public static readonly BindableProperty XAxesProperty =
        BindableProperty.Create(nameof(XAxes), typeof(IEnumerable<ICartesianAxis>), typeof(ChartContainerView), default);

    public IEnumerable<ICartesianAxis> XAxes
	{
		get => (IEnumerable<ICartesianAxis>)GetValue(XAxesProperty);
		set => SetValue(XAxesProperty, value);
	}

	public static readonly BindableProperty YAxesProperty =
		BindableProperty.Create(nameof(YAxes), typeof(IEnumerable<ICartesianAxis>), typeof(ChartContainerView), default);

	public IEnumerable<ICartesianAxis> YAxes
	{
		get => (IEnumerable<ICartesianAxis>)GetValue(YAxesProperty);
		set => SetValue(YAxesProperty, value);
    }

	public static readonly BindableProperty LegendPositionProperty =
		BindableProperty.Create(nameof(LegendPosition), typeof(LegendPosition), typeof(ChartContainerView), default);

	public LegendPosition LegendPosition
	{
		get => (LegendPosition)GetValue(LegendPositionProperty);
		set => SetValue(LegendPositionProperty, value);
	}

	public static readonly BindableProperty LegendTextPaintProperty =
		BindableProperty.Create(nameof(LegendTextPaint), typeof(IPaint<SkiaSharpDrawingContext>), typeof(ChartContainerView), default);

	public IPaint<SkiaSharpDrawingContext> LegendTextPaint
	{
		get => (IPaint<SkiaSharpDrawingContext>)GetValue(LegendTextPaintProperty);
		set => SetValue(LegendTextPaintProperty, value);
	}
    
    public ChartContainerView()
    {
        InitializeComponent();
		Chart.TooltipTextPaint = ChartPaints.TooltipTextPaint;
		Chart.TooltipBackgroundPaint = ChartPaints.TooltipBackgroundPaint;
    }

    public void UpdateChart() => Chart.CoreChart.Update();
}