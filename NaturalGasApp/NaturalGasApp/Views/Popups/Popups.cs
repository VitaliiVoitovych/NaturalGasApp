using NaturalGasApp.Services.QrCode;

namespace NaturalGasApp.Views.Popups;

public static class Popups
{
    public static QrCodePopup ShareAppQrCodePopup => new("Поділитися застосунком", QrCodes.ShareAppQrCode);
}
