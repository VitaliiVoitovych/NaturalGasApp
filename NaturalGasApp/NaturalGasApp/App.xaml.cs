
namespace NaturalGasApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

    protected override void OnStart()
    {
		UserAppTheme = AppTheme.Dark;

        base.OnStart();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
