using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SpotifyUI;
using SpotifyUI.Services;
using SpotifyUI.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddHttpClient("SpotifyClient", client => { client.BaseAddress = new Uri("https://accounts.spotify.com/"); });
builder.Services.AddScoped<ISpotifyAuthenticationService, SpotifyAuthenticationService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
