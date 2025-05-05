using FitZone.Client.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Net.Maui.Controls;

namespace FitZone.Client.Services
{
    public class QrCodeScannerService : IQrCodeScannerService
    {
        public TaskCompletionSource<string> _tcs;

        public Task<string> ScanQrCodeAsync()
        {
            _tcs = new TaskCompletionSource<string>();

            var scanPage = new ScanPage(result =>
            {
                _tcs.TrySetResult(result);
            });

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(scanPage);
            });

            return _tcs.Task;
        }
    }
}
