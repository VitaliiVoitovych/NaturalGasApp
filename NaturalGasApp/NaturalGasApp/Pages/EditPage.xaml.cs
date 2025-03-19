namespace NaturalGasApp.Pages;

public partial class EditPage : ContentPage
{
    public EditPage(EditViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}