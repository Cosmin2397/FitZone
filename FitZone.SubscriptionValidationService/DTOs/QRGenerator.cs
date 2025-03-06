using FitZone.SubscriptionValidationService.Models;
using Newtonsoft.Json;
using ZXing.QrCode;
using ZXing;
using ZXing.Windows.Compatibility;

namespace FitZone.SubscriptionValidationService.DTOs
{

    public static class QRGenerator
    {
        public static byte[] GenerateQrCode(ClientsAccess access)
        {
            string json = JsonConvert.SerializeObject(access);

            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Width = 300,
                    Height = 300,
                    Margin = 1
                }
            };

            using var bitmap = writer.Write(json);
            using var ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            return ms.ToArray(); 
        }
    }

}
