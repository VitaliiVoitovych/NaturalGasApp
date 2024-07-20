using Microsoft.Maui.Handlers;

namespace NaturalGasApp.Droid;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		AddControlsHandlers();
		
		builder
			.UseSharedMauiApp();

		return builder.Build();
	}
	
	private static void AddControlsHandlers()
	{
		EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
		{
			handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.ParseColor("#e2e3ec"));
		});

		PickerHandler.Mapper.AppendToMapping(nameof(Picker), (handler, view) =>
		{
			handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.ParseColor("#e2e3ec"));
		});

		StepperHandler.Mapper.AppendToMapping(nameof(Stepper), (handler, view) =>
		{
			var buttonBackgroundColor = Android.Graphics.Color.ParseColor("#1C2236");
			var enabledTextColor = Android.Graphics.Color.ParseColor("#e2e3ec");
			var disabledTextColor = Android.Graphics.Color.ParseColor("#696d70");

			var states = new int[][]
			{
				[Android.Resource.Attribute.StateEnabled],
				[-Android.Resource.Attribute.StateEnabled],
			};

			int[] colors = [enabledTextColor, disabledTextColor];

			var textColorList = new Android.Content.Res.ColorStateList(states, colors);

			var linearLayout = handler.PlatformView as Android.Widget.LinearLayout;
			var button1 = linearLayout.GetChildAt(0) as Android.Widget.Button;
			var button2 = linearLayout.GetChildAt(1) as Android.Widget.Button;

			button1!.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(buttonBackgroundColor);
			button2!.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(buttonBackgroundColor);

			button1!.SetTextColor(textColorList);
			button2!.SetTextColor(textColorList);
		});
	}
}
