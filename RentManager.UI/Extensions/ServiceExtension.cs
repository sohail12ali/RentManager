using AutoMapper;

using RentManager.DataAccess.DataServices;
using RentManager.DataAccess.Mapper.Profiles;

namespace RentManager.UI.Extensions;

internal static class ServiceExtension
{
    public static MauiAppBuilder AddServices(this MauiAppBuilder builder)
    {
        builder.AddOtherServices();
        builder.AddViewModels();
        builder.AddPages();
        builder.AddMapper();
        builder.ConfigureFontsServices();
        return builder;
    }

    private static MauiAppBuilder AddOtherServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IDataService, DataService>();
        return builder;
    }

    private static MauiAppBuilder AddPages(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainPage>();
        return builder;
    }

    private static MauiAppBuilder AddViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainViewModel>();
        return builder;
    }

    private static MauiAppBuilder AddMapper(this MauiAppBuilder builder)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ElectricityBillProfile());
            mc.AddProfile(new PayingGuestProfile());
            mc.AddProfile(new RentDetailProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();

        builder.Services.AddSingleton<IMapper>(mapper);
        return builder;
    }

    private static MauiAppBuilder ConfigureFontsServices(this MauiAppBuilder builder)
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
