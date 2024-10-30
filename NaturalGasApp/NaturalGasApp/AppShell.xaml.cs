using System.Globalization;

namespace NaturalGasApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("uk-Ua");

		CurrentItem = PhoneTabs;
	}
}
