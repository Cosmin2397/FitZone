using FitZone.Client.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Net.Maui.Controls;

namespace FitZone.Client.Shared.Services
{
    public class FallbackQrCodeScannerService : IQrCodeScannerService
    {
        public Task<string> ScanQrCodeAsync()
        {
            return Task.FromResult("Fallback: QR not supported.");
        }
    }
}
