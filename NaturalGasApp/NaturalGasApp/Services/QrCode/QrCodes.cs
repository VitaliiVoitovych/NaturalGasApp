namespace NaturalGasApp.Services.QrCode;

public static class QrCodes
{
    private const string Version = "1.0.10";
    private const string AppDownloadUrl = $@"https://github.com/VitaliiVoitovych/NaturalGasApp/releases/download/v{Version}/NaturalGasApp-v{Version}.apk";

    public static readonly ImageSource ShareAppQrCode = QrCodeService.GenerateQrCode(AppDownloadUrl);
}
