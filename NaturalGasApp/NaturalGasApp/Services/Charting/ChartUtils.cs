using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using NaturalGasApp.Services.Charting.Styles;
using SkiaSharp;

namespace NaturalGasApp.Services.Charting;

public static class ChartUtils
{
    public static Axis CreateValueYAxis(Func<double, string>? labeler = default)
    {
        return new Axis
        {
            SeparatorsPaint = ChartPaints.SeparatorPaint,
            Labeler = labeler ?? Labelers.Default,
            TextSize = ChartConstants.AxisTextSize,
            LabelsPaint = ChartPaints.AxisLabelsPaint,
        };
    }
    public static LineSeries<TValues> CreateLineSeries<TValues>(ObservableCollection<TValues> values, SKColor color, string? name = default)
    {
        return new LineSeries<TValues>
        {
            Name = name,
            Values = values,
            Fill = null,
            Stroke = new SolidColorPaint(color) { StrokeThickness = ChartConstants.DefaultStrokeThickness },
            GeometryFill = new SolidColorPaint(color),
            GeometryStroke = new SolidColorPaint(color) { StrokeThickness = ChartConstants.DefaultStrokeThickness },
            GeometrySize = ChartConstants.DefaultGeometrySize,
        };
    }
}
