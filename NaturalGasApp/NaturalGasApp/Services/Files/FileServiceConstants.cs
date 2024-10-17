namespace NaturalGasApp.Services.Files;

public static class FileServiceConstants
{
    public const string FilePickerTitle = "Виберіть файл з даними (electricity.json)";
    public const string ShareFileTitle = "Експортувати дані";
    public const string OpenFileExceptionMessage = "Не було обрано файл";
    public static readonly FilePickerFileType FilePickerFileType = new(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, ["application/json"] }
            });
}
