namespace NaturalGasApp.ViewModels;

public partial class MainViewModel(NotesService _notesService) : ObservableObject
{
    public ChartsService ChartsService => _notesService.ChartsService;

    public NotesService NotesService => _notesService;
}