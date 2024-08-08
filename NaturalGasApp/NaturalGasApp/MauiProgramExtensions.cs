using Microsoft.Extensions.Logging;
using NaturalGasApp.EfStructures;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace NaturalGasApp;

public static class MauiProgramExtensions
{
	public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
	{
		builder
			.UseSkiaSharp(true)
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.ConfigureEssentials(essentials =>
			{
				essentials
					.AddAppAction("share_app", "Поділитися", icon: "qr_code")
					.AddAppAction("add_record", "Додати запис", icon: "add_action")
					.OnAppAction(HandleAppActions);
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddNaturalGasDbContext("naturalgas.db");

		builder.Services.AddSingleton<ChartsService>();
		builder.Services.AddSingleton<NotesService>();
		
		builder.Services.AddTransient<MainViewModel>();
		builder.Services.AddTransient<NotesViewModel>();
		builder.Services.AddTransient<AddViewModel>();
		builder.Services.AddSingleton<QrCodeViewModel>();

		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<NotesPage>();
		builder.Services.AddTransient<AddPage>();
		builder.Services.AddTransient<QrCodePage>();
		
		return builder;
	}

	private static void HandleAppActions(AppAction action)
	{
		Application.Current?.Dispatcher.Dispatch(async () =>
		{
			if (action.Id == "add_record")
			{
				var tabBar = Shell.Current.CurrentItem; // TabBar
				var addTab = tabBar.Items[^1]; // Add tab is last

				tabBar.CurrentItem = addTab;
			}
			else
			{
                await Task.Delay(250);
                await Shell.Current.GoToAsync($"{nameof(QrCodePage)}", true);
            }
		});
	}
}
