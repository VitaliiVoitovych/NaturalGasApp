using QRCoder;

namespace NaturalGasApp.Services.QrCode;

public static class QrCodeService
{
    public static ImageSource GenerateQrCode(string url, int pixelsPerModule = 20)
    {
        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new PngByteQRCode(qrCodeData);
        var qrCodeAsPngByteArray = qrCode.GetGraphic(pixelsPerModule);

        return ImageSource.FromStream(() => new MemoryStream(qrCodeAsPngByteArray));
    }
}
