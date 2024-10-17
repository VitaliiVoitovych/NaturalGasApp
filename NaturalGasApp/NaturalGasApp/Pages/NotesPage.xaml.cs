namespace NaturalGasApp.Pages;

public partial class NotesPage : ContentPage
{
    public NotesPage(NotesViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        CommunityToolkit.Maui.Core.Platform.StatusBar.SetColor(Color.FromArgb("#0D121C"));
    }
}