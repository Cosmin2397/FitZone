using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Net.Maui.Controls;

namespace FitZone.Client.Services
{
    public class ScanPage : ContentPage
    {
        private readonly CameraBarcodeReaderView _cameraView;

        public ScanPage(Action<string> onDetected)
        {
            _cameraView = new CameraBarcodeReaderView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "cameraView",
                IsTorchOn = false,
                IsDetecting = true
            };

            _cameraView.BarcodesDetected += (s, e) =>
            {
                var result = e.Results.FirstOrDefault()?.Value;
                if (!string.IsNullOrEmpty(result))
                {
                    onDetected(result);
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current.MainPage.Navigation.PopModalAsync();
                    });
                }
            };

            Content = _cameraView;
        }
    }
}
