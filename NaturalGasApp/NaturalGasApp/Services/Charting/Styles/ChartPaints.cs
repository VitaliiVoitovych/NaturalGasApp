using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.Painting;

namespace NaturalGasApp.Services.Charting.Styles;

public static class ChartPaints
{
    public static readonly SolidColorPaint AxisLabelsPaint = new(ChartColors.AxisLabelsColor);
    public static readonly SolidColorPaint SeparatorPaint = new(ChartColors.SeparatorColor)
    {
        StrokeThickness = ChartConstants.SeparatorStrokeThickness,
        PathEffect = new DashEffect(ChartConstants.SeparatorDashPattern)
    };
    public static readonly SolidColorPaint LegendTextPaint = new(ChartColors.LegendTextColor);

    public static readonly SolidColorPaint TooltipTextPaint = new(ChartColors.TooltipTextColor);
    public static readonly SolidColorPaint TooltipBackgroundPaint = new(ChartColors.TooltipBackgroundColor);
}
