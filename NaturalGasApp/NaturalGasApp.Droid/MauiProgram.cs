using NaturalGasApp.Droid.Handlers;

namespace NaturalGasApp.Droid;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.ConfigureMauiHandlers(handlers =>
			{
				handlers.AddHandler<StepperWithInput, StepperWithInputHandler>();
				handlers.AddHandler<WheelDatePicker, WheelDatePickerHandler>();
			})
			.UseSharedMauiApp();

		return builder.Build();
	}
}
