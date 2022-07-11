using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace SpeakersApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		var a = Assembly.GetExecutingAssembly();
		using var stream = a.GetManifestResourceStream("SpeakersApp.appsettings.json");

		var config = new ConfigurationBuilder()
					.AddJsonStream(stream)
					.Build();


		builder.Configuration.AddConfiguration(config);

		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddTransient<SpeakersApiClient.SpeakersApiClient>();
		builder.Services.AddTransient<MainPage>();

		return builder.Build();
	}
}

