using Microsoft.Extensions.Logging;
using FitZone.Client.Services;
using FitZone.Client.Shared.Services.Interfaces;
using FitZone.Client.Shared;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace FitZone.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseBarcodeReader()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Add device-specific services used by the FitZone.Client.Shared project
        builder.Services.AddSingleton<IFormFactor, FormFactor>();
        builder.Services.AddSharedServices(builder.Configuration);
#if ANDROID
        builder.Services.AddSingleton<IQrCodeScannerService, QrCodeScannerService>();
#endif
        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
