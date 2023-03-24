using RentManager.UI.Extensions;

namespace RentManager.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        AppDomain.CurrentDomain.UnhandledException += UnhandledException;
        var builder = MauiApp.CreateBuilder()
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .AddServices();
        var app = builder.Build();

        return app;
    }

    private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        Debug.WriteLine(e.ExceptionObject.ToString());
    }
}