using Application.Contracts.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using MobileApp.Services;
using Ui.Shared.Services;

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

		builder.Services.AddMauiBlazorWebView();

        builder.Services.AddSingleton<ITokenService, SecureTokenService>();
        builder.Services.AddTransient<JwtAuthorizationHandler>();

        builder.Services.AddHttpClient("ApiClient", c =>
        {
            c.BaseAddress = new Uri("http://<your-pc-lan-ip>:5000/");
        }).AddHttpMessageHandler<JwtAuthorizationHandler>();

        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));


#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
