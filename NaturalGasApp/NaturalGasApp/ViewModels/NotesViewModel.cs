using CommunityToolkit.Maui.Views;
using NaturalGasApp.Services.Files;
using NaturalGasApp.Views.Popups;
using System.Text.Json;

namespace NaturalGasApp.ViewModels;

public partial class NotesViewModel(NotesService notesService, FileService fileService) : ObservableObject
{
    public NotesService NotesService => notesService;

    [RelayCommand]
    private void Remove(NaturalGasConsumption consumption)
    {
        NotesService.RemoveNote(consumption);
    }

    [RelayCommand]
    private async Task ShowQrCodePopup()
    {
        await Shell.Current.ShowPopupAsync(Popups.ShareAppQrCodePopup);
    }
    
    [RelayCommand]
    private async Task ExportData()
    {
        var filename = $"naturalgas_{DateTime.UtcNow:dd-MM-yyyy}.json";
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), filename);

        await DataExporterImporter.ExportAsync(filePath, NotesService.NaturalGasConsumptions);

        await fileService.ShareFileAsync(filePath);
        try
        {
            File.Delete(filePath);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Помилка", ex.Message, "Зрозуміло");
        }
    }

    [RelayCommand]
    private async Task ImportData()
    {
        try
        {
            var file = await fileService.OpenFileAsync();
            var data = await DataExporterImporter.ImportAsync(file);
            await NotesService.ImportDataAsync(data);
        }
        catch (FileNotFoundException ex)
        {
            await Shell.Current.DisplayAlert("Помилка!", ex.Message, "Зрозуміло");
        }
        catch (JsonException)
        {
            await Shell.Current.DisplayAlert("Помилка!", "Ви обрали не файл з даними!", "Зрозуміло");
        }
    }
}