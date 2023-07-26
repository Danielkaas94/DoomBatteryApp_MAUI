using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace DoomBatteryApp_MAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			// Initialize the .NET MAUI Community Toolkit MediaElement by adding the below line of code
			.UseMauiCommunityToolkitMediaElement()
            // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts, and other things
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
