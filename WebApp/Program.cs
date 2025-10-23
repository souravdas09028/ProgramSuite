using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebApp;
using Ui.Shared.Services;
using Blazored.LocalStorage;
using Application.Contracts.Services;
using WebApp.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HttpClient directly (no IHttpClientFactory in WASM)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5239/") // e.g. https://localhost:5000/
});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ITokenService, LocalTokenService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();

await builder.Build().RunAsync();
