using Microsoft.Extensions.Logging;
using Ui.Shared.Services;
using Application.Contracts.Services;
using MobileApp.Services;

namespace MobileApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Register Token Service (Secure Storage)
        builder.Services.AddSingleton<ITokenService, SecureTokenService>();

        // Register HttpClientFactory + JWT Handler
        builder.Services.AddTransient<JwtAuthorizationHandler>();
        builder.Services.AddHttpClient("ApiClient", client =>
        {
            client.BaseAddress = new Uri("http://localhost:5239/");
        }).AddHttpMessageHandler<JwtAuthorizationHandler>();

        // Scoped HttpClient using factory
        builder.Services.AddScoped(sp =>
            sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
