using NaturalGasApp.Services.Charting;

namespace NaturalGasApp.ViewModels;

public partial class MainViewModel(NotesService notesService) : ObservableObject
{
    public NotesService NotesService => notesService;
    public ChartsService ChartsService => NotesService.ChartsService;
}