using TextInputEditText = Google.Android.Material.TextField.TextInputEditText;
using AButton = Android.Widget.Button;
using ColorStateList = Android.Content.Res.ColorStateList;
using InputTypes = Android.Text.InputTypes;
namespace NaturalGasApp.Droid.Handlers;

public class StepperWithInputHandler : StepperHandler
{
    protected AButton? UpButton { get; private set; }
    protected AButton? DownButton { get; private set; }
    protected TextInputEditText? TextField { get; private set; }
    public new StepperWithInput VirtualView => (StepperWithInput)base.VirtualView;

    public static IPropertyMapper<IStepper, IStepperHandler> PropertyMapper = new PropertyMapper<StepperWithInput, StepperWithInputHandler>(ViewHandler.ViewMapper)
    {
        [nameof(ITextStyle.TextColor)] = MapTextColor,
        [nameof(ITextStyle.Font)] = MapFont,
        [nameof(StepperWithInput.Value)] = MapValue,
        [nameof(StepperWithInput.Increment)] = MapIncrement,
        [nameof(StepperWithInput.UnderlineColor)] = MapUnderlineColor,
        [nameof(StepperWithInput.ButtonBackgroundColor)] = MapButtonBackgroundColor,
        [nameof(StepperWithInput.ButtonForegroundColor)] = MapButtonTextColor,
    };

    protected override MauiStepper CreatePlatformView()
    {
        var stepper = base.CreatePlatformView();

        UpButton = (AButton)stepper.GetChildAt(stepper.ChildCount - 1)!;
        DownButton = (AButton)stepper.GetChildAt(stepper.ChildCount - 2)!;

        TextField = new TextInputEditText(Context)
        {
            InputType = InputTypes.ClassNumber | InputTypes.NumberFlagSigned,
        };
        TextField.SetMinWidth((int)Context.ToPixels(120));

        stepper.AddView(TextField, 0);

        return stepper;
    }

    public StepperWithInputHandler() : base(PropertyMapper)
    {

    }

    public StepperWithInputHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
    {

    }

    protected override void ConnectHandler(MauiStepper platformView)
    {
        base.ConnectHandler(platformView);

        TextField!.TextChanged += OnEntryTextChanged;
    }

    protected override void DisconnectHandler(MauiStepper platformView)
    {
        base.DisconnectHandler(platformView);

        TextField!.TextChanged -= OnEntryTextChanged;
    }

    private void OnEntryTextChanged(object? sender, Android.Text.TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TextField!.Text))
        {
            VirtualView.Value = 0.0;
            TextField.Text = $"{VirtualView.Value}";
            return;
        }
        if (double.TryParse(TextField.Text, out var value))
        {
            if (value >= VirtualView.Minimum && value <= VirtualView.Maximum)
            {
                VirtualView.Value = value;
                TextField.SetSelection(TextField.Text.Length);
            }
        }
    }

    public static void MapValue(StepperWithInputHandler handler, StepperWithInput stepper)
    {
        handler.PlatformView.UpdateValue(stepper);

        handler.TextField!.Text = stepper.Value.ToString();
    }

    public static void MapIncrement(StepperWithInputHandler handler, StepperWithInput stepper)
    {
        handler.PlatformView?.UpdateIncrement(stepper);

        if (!double.IsInteger(stepper.Increment))
        {
            handler.TextField!.InputType |= InputTypes.NumberFlagDecimal;
            handler.TextField.KeyListener = LocalizedDigitsKeyListener.Create(handler.TextField.InputType);
        }
    }

    private static void MapUnderlineColor(StepperWithInputHandler handler, StepperWithInput stepper)
    {
        // TODO: Text field Cursor styling: Color (Solved. Need Review) | need API 29
        //handler.TextField!.TextCursorDrawable.SetTintList(ColorStateList.ValueOf(stepper.UnderlineColor.ToPlatform()));
        handler.TextField!.BackgroundTintList = ColorStateList.ValueOf(stepper.UnderlineColor.ToPlatform());
    }

    private static void MapButtonBackgroundColor(StepperWithInputHandler handler, StepperWithInput stepper)
    {
        var buttonBackgroundColor = stepper.ButtonBackgroundColor.ToPlatform();

        handler.UpButton!.BackgroundTintList = ColorStateList.ValueOf(buttonBackgroundColor);
        handler.DownButton!.BackgroundTintList = ColorStateList.ValueOf(buttonBackgroundColor);
    }

    private static void MapTextColor(StepperWithInputHandler handler, StepperWithInput stepper)
    {
        handler.TextField!.UpdateTextColor(stepper);
    }

    private static void MapFont(StepperWithInputHandler handler, StepperWithInput stepper)
    {
        var fontManager = handler.MauiContext?.Services.GetRequiredService<IFontManager>()!;
        handler.TextField!.UpdateFont(stepper, fontManager);

        handler.UpButton!.UpdateFont(stepper, fontManager);
        handler.DownButton!.UpdateFont(stepper, fontManager);
    }

    private static void MapButtonTextColor(StepperWithInputHandler handler, StepperWithInput stepper)
    {
        var enabledTextColor = stepper.ButtonForegroundColor.ToPlatform();
        var disabledTextColor = Android.Graphics.Color.Argb(130, enabledTextColor.R, enabledTextColor.G, enabledTextColor.B);

        var states = new int[][]
        {
            [Android.Resource.Attribute.StateEnabled],
            [-Android.Resource.Attribute.StateEnabled],
        };

        int[] colors = [enabledTextColor, disabledTextColor];

        var textColorList = new ColorStateList(states, colors);

        handler.UpButton!.SetTextColor(textColorList);
        handler.DownButton!.SetTextColor(textColorList);
    }
}
