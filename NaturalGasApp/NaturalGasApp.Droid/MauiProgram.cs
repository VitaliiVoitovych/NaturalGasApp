using NaturalGasApp.Droid.Handlers;

namespace NaturalGasApp.Droid;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		AddControlsHandlers();

		builder
			.ConfigureMauiHandlers(handlers =>
			{
				handlers.AddHandler<StepperWithInput, StepperWithInputHandler>();
				handlers.AddHandler<WheelDatePicker, WheelDatePickerHandler>();
			})
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
	}
}
