using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace NaturalGasApp.Droid;

[IntentFilter(new[] { Platform.Intent.ActionAppAction },
    Categories = new[] { global::Android.Content.Intent.CategoryDefault })]
[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnResume()
    {
        base.OnResume();

        Platform.OnResume(this);
    }

    protected override void OnNewIntent(Intent? intent)
    {
        base.OnNewIntent(intent);

        Platform.OnNewIntent(intent);
    }

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        SetTheme(Resource.Style.AppTheme);
    }
}
