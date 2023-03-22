using RentManager.UI.Extensions;

namespace RentManager.UI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder()
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .AddServices();
		return builder.Build();
	}
}
