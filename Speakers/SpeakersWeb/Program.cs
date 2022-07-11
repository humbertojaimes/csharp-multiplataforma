using Blazor.Extensions.Logging;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SpeakersWeb;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddLogging(builder => builder
        .AddBrowserConsole());
builder.Logging.SetMinimumLevel(LogLevel.Warning);


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<SpeakersApiClient.SpeakersApiClient>();

await builder.Build().RunAsync();

