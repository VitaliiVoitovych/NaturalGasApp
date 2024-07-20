namespace NaturalGasApp.Pages;

public partial class QrCodePage : ContentPage
{
    public QrCodePage(QrCodeViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}