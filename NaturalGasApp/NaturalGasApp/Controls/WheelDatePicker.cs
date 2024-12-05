using Font = Microsoft.Maui.Font;
using IFontElement = Microsoft.Maui.Controls.Internals.IFontElement;

namespace NaturalGasApp.Controls;

public class WheelDatePicker : View, ITextStyle, IFontElement
{
    #region ITextStyle property
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ITextStyle), Colors.Gray);
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public Font Font => this.ToFont();

    public static readonly BindableProperty CharacterSpacingProperty = InputView.CharacterSpacingProperty;
    public double CharacterSpacing
    {
        get => (double)GetValue(CharacterSpacingProperty);
        set => SetValue(CharacterSpacingProperty, value);
    }
    #endregion

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(WheelDatePicker), default);
    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty IsDayVisibleProperty = BindableProperty.Create(nameof(IsDayVisible), typeof(bool), typeof(WheelDatePicker), true);
    public bool IsDayVisible
    {
        get => (bool)GetValue(IsDayVisibleProperty);
        set => SetValue(IsDayVisibleProperty, value);
    }

    public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(nameof(SelectedDate), typeof(DateOnly), typeof(WheelDatePicker), DateOnly.FromDateTime(DateTime.Now), BindingMode.TwoWay);
    public DateOnly SelectedDate
    {
        get => (DateOnly)GetValue(SelectedDateProperty);
        set => SetValue(SelectedDateProperty, value);
    }

    public static readonly BindableProperty UnderlineColorProperty = BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(StepperWithInput), Colors.Gray);
    public Color UnderlineColor
    {
        get => (Color)GetValue(UnderlineColorProperty);
        set => SetValue(UnderlineColorProperty, value);
    }

    #region Font Property
    public static readonly BindableProperty FontAttributesProperty = InputView.FontAttributesProperty;
    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }

    public static readonly BindableProperty FontFamilyProperty = InputView.FontFamilyProperty;
    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    public static readonly BindableProperty FontSizeProperty = InputView.FontSizeProperty;
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public static readonly BindableProperty FontAutoScalingEnabledProperty = InputView.FontAutoScalingEnabledProperty;
    public bool FontAutoScalingEnabled
    {
        get => (bool)GetValue(FontAutoScalingEnabledProperty);
        set => SetValue(FontAutoScalingEnabledProperty, value);
    }
    #endregion

    public void OnFontFamilyChanged(string oldValue, string newValue) => HandleFontChanged();

    public void OnFontSizeChanged(double oldValue, double newValue) => HandleFontChanged();

    public void OnFontAutoScalingEnabledChanged(bool oldValue, bool newValue) => HandleFontChanged();

    public double FontSizeDefaultValueCreator() => 14;

    public void OnFontAttributesChanged(FontAttributes oldValue, FontAttributes newValue) => HandleFontChanged();

    void HandleFontChanged()
    {
        InvalidateMeasure();
    }
}