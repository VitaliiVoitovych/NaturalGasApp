using Android.Content;
using NaturalGasApp.Droid.Controls;
using MaterialAlertDialogBuilder = Google.Android.Material.Dialog.MaterialAlertDialogBuilder;
using TextInputEditText = Google.Android.Material.TextField.TextInputEditText;
using ColorStateList = Android.Content.Res.ColorStateList;

namespace NaturalGasApp.Droid.Handlers;

public class WheelDatePickerHandler : ViewHandler<WheelDatePicker, TextInputEditText>
{
    internal DateOnly Date { get; private set; }
    internal string? DateFormat { get; private set; }
    internal TextInputEditText? TextField { get; private set; }

    public static IPropertyMapper<WheelDatePicker, WheelDatePickerHandler> PropertyMapper = new PropertyMapper<WheelDatePicker, WheelDatePickerHandler>(ViewMapper)
    {
        [nameof(WheelDatePicker.IsDayVisible)] = MapIsDayVisible, // Always First
        [nameof(WheelDatePicker.SelectedDate)] = MapSelectedDate,
        [nameof(WheelDatePicker.UnderlineColor)] = MapUnderlineColor,
        [nameof(ITextStyle.TextColor)] = MapTextColor,
        [nameof(ITextStyle.Font)] = MapFont,
    };

    public WheelDatePickerHandler() : base(PropertyMapper)
    {

    }

    public WheelDatePickerHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
    {
    }

    protected override TextInputEditText CreatePlatformView()
    {
        TextField = new TextInputEditText(Context)
        {
            TextAlignment = Android.Views.TextAlignment.Center,
            InputType = Android.Text.InputTypes.Null,
            Focusable = true,
            Clickable = true,
        };

        return TextField;
    }

    protected override void ConnectHandler(TextInputEditText platformView)
    {
        platformView.FocusChange += PlatformView_FocusChange;
        platformView.Click += PlatformView_Click;

        base.ConnectHandler(platformView);
    }

    protected override void DisconnectHandler(TextInputEditText platformView)
    {
        platformView.FocusChange -= PlatformView_FocusChange;
        platformView.Click -= PlatformView_Click;

        base.DisconnectHandler(platformView);
    }

    private void PlatformView_FocusChange(object? sender, Android.Views.View.FocusChangeEventArgs e)
    {
        if (e.HasFocus)
        {
            if (PlatformView.Clickable)
                PlatformView.CallOnClick();
            else
                PlatformView_Click(PlatformView, EventArgs.Empty);
        }
    }

    private void PlatformView_Click(object? sender, EventArgs e)
    {
        var datePickerDialogContainer = new DatePickerDialogContainer(Context, Date);
        datePickerDialogContainer.SetDayPickerVisibility(VirtualView.IsDayVisible);

        using var builder = new MaterialAlertDialogBuilder(Context, Resource.Style.CustomMaterialDialogStyle);

        builder.SetTitle(VirtualView.Title);
        builder.SetNegativeButton(Android.Resource.String.Cancel, (o, args) => { });
        builder.SetPositiveButton("Вибрати", (s, args) => VirtualView.SelectedDate = datePickerDialogContainer.SelectedDate);

        builder.SetView(datePickerDialogContainer);

        var dialog = builder.Create();
        dialog.Show();
    }

    private static void MapSelectedDate(WheelDatePickerHandler handler, WheelDatePicker picker)
    {
        handler.Date = picker.SelectedDate;

        handler.PlatformView.Text = picker.SelectedDate.ToString(handler.DateFormat);
    }

    private static void MapIsDayVisible(WheelDatePickerHandler handler, WheelDatePicker picker)
    {
        handler.DateFormat = picker.IsDayVisible ? "dd MMMM yyyy" : "MMMM yyyy";
    }

    private static void MapUnderlineColor(WheelDatePickerHandler handler, WheelDatePicker picker)
    {
        handler.TextField!.BackgroundTintList = ColorStateList.ValueOf(picker.UnderlineColor.ToPlatform());
    }

    private static void MapTextColor(WheelDatePickerHandler handler, WheelDatePicker picker)
    {
        handler.TextField!.UpdateTextColor(picker);
    }

    private static void MapFont(WheelDatePickerHandler handler, WheelDatePicker picker)
    {
        var fontManager = handler.MauiContext?.Services.GetRequiredService<IFontManager>()!;
        handler.TextField!.UpdateFont(picker, fontManager);
    }
}
