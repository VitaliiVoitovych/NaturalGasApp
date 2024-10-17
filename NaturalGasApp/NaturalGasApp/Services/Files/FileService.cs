namespace NaturalGasApp.Services.Files;

public class FileService
{
    private readonly PickOptions pickOptions = new PickOptions
    {
        PickerTitle = FileServiceConstants.FilePickerTitle,
        FileTypes = FileServiceConstants.FilePickerFileType,
    };
    public async Task<Stream> OpenFileAsync()
    {
        var result = await FilePicker.Default.PickAsync(pickOptions) ?? throw new FileNotFoundException(FileServiceConstants.OpenFileExceptionMessage);
        return await result.OpenReadAsync();
    }
    public async Task ShareFileAsync(string filePath)
    {
        await Share.Default.RequestAsync(new ShareFileRequest
        {
            Title = FileServiceConstants.ShareFileTitle,
            File = new ShareFile(filePath)
        });
    }
}
