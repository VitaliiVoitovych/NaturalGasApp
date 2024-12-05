using Android.Content;
using Android.Views;
using Android.Widget;
using System.Globalization;

namespace NaturalGasApp.Droid.Controls;

public class DatePickerDialogContainer : LinearLayout
{
    private static readonly ViewGroup.LayoutParams NumberPickerLayoutParams = new LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent)
    {
        LeftMargin = 20,
        RightMargin = 20,
        Weight = 1f,
    };

    private static readonly string[] Months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;

    private NumberPicker _dayPicker = null!;
    private NumberPicker _monthPicker = null!;
    private NumberPicker _yearPicker = null!;

    public DateOnly SelectedDate => new(_yearPicker.Value, _monthPicker.Value, _dayPicker.Value);

    public DatePickerDialogContainer(Context? context, DateOnly date) : base(context)
    {
        InitializeContainer();

        InitializeComponents();

        SetValueForPickers(date);

        AddPickers(_dayPicker, _monthPicker, _yearPicker);
    }

    private void InitializeContainer()
    {
        Orientation = Orientation.Horizontal;
        SetGravity(GravityFlags.CenterHorizontal);
        SetPadding(20, 0, 20, 0);
    }

    private void InitializeComponents()
    {
        _dayPicker = CreateNumberPicker(1, 30);

        _monthPicker = CreateNumberPicker(1, 12);

        _monthPicker.SetDisplayedValues(Months);
        _monthPicker.ValueChanged += OnValueChanged;

        var year = DateTime.Now.Year;
        _yearPicker = CreateNumberPicker(year - 10, year);
        _yearPicker.ValueChanged += OnValueChanged;
    }

    private void AddPickers(params NumberPicker[] pickers)
    {
        foreach (var picker in pickers)
            AddView(picker, NumberPickerLayoutParams);
    }

    private void SetValueForPickers(DateOnly date) => (_yearPicker!.Value, _monthPicker!.Value, _dayPicker!.Value) = date; // Sets value for pickers

    private NumberPicker CreateNumberPicker(int minValue, int maxValue)
    {
        return new(Context)
        {
            MinValue = minValue,
            MaxValue = maxValue,
        };
    }

    private void OnValueChanged(object? sender, NumberPicker.ValueChangeEventArgs e)
    {
        _dayPicker.MaxValue = CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(_yearPicker.Value, _monthPicker.Value);
    }

    public void SetDayPickerVisibility(bool isDayVisible)
    {
        _dayPicker.Visibility = isDayVisible ? ViewStates.Visible : ViewStates.Gone;
    }
}
