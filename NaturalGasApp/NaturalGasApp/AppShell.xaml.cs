using System.Globalization;

namespace NaturalGasApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("uk-Ua");
		
		Routing.RegisterRoute(nameof(QrCodePage), typeof(QrCodePage));

		CurrentItem = PhoneTabs;
		Application.Current!.UserAppTheme = AppTheme.Dark;
	}
}
