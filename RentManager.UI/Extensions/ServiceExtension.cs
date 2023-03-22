namespace RentManager.UI.Extensions;

public static class ServiceExtension
{
    public static MauiAppBuilder AddServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();

        return builder;
    }

    public static MauiAppBuilder ConfigureFontsServices(this MauiAppBuilder builder)
    {
        builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("FontAwesome6FreeBrands.otf", "FontAwesomeBrands");
            fonts.AddFont("FontAwesome6FreeRegular.otf", "FontAwesomeRegular");
            fonts.AddFont("FontAwesome6FreeSolid.otf", "FontAwesomeSolid");
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });  
        return builder;
    }
}
