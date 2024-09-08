namespace NaturalGasApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    protected override void OnStart()
    {
		UserAppTheme = AppTheme.Dark;

        base.OnStart();
    }
}
