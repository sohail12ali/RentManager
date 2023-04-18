using AutoMapper;

using Microsoft.Extensions.Logging;

using RentManager.DataAccess.DataServices;
using RentManager.DataAccess.Mapper.Profiles;

namespace RentManager.UI.Extensions;

internal static class ServiceExtension
{
    public static MauiAppBuilder AddServices(this MauiAppBuilder builder)
    {
        _ = builder.AddLoggingServices();
        _ = builder.AddOtherServices();
        _ = builder.AddViewModels();
        _ = builder.AddPages();
        _ = builder.AddMapper();
        _ = builder.ConfigureFontsServices();
        return builder;
    }

    private static MauiAppBuilder AddOtherServices(this MauiAppBuilder builder)
    {
        _ = builder.Services.AddSingleton<IDataService, DataService>();
        return builder;
    }

    private static MauiAppBuilder AddLoggingServices(this MauiAppBuilder builder)
    {
        _ = builder.Services.AddLogging(configure =>
        {
            configure.AddDebug();
        });
        return builder;
    }

    private static MauiAppBuilder AddPages(this MauiAppBuilder builder)
    {
        _ = builder.Services.AddSingleton<AppShell>();
        _ = builder.Services.AddSingleton<MainPage>();
        _ = builder.Services.AddTransient<AddGuestPage>();
        _ = builder.Services.AddTransient<ViewGuestPage>();
        _ = builder.Services.AddTransient<AddBillPage>();
        _ = builder.Services.AddTransient<ViewBillsPage>();
        return builder;
    }

    private static MauiAppBuilder AddViewModels(this MauiAppBuilder builder)
    {
        _ = builder.Services.AddSingleton<AppShellViewModel>();
        _ = builder.Services.AddSingleton<MainViewModel>();
        _ = builder.Services.AddTransient<AddGuestViewModel>();
        _ = builder.Services.AddTransient<ViewGuestViewModel>();
        _ = builder.Services.AddTransient<AddBillViewModel>();
        _ = builder.Services.AddSingleton<ViewBillsViewModel>();
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

        builder.Services.AddSingleton(mapper);
        return builder;
    }

    private static MauiAppBuilder ConfigureFontsServices(this MauiAppBuilder builder)
    {
        _ = builder.ConfigureFonts(fonts =>
        {
            _ = fonts.AddFont("FontAwesome6FreeBrands.otf", "FontAwesomeBrands");
            _ = fonts.AddFont("FontAwesome6FreeRegular.otf", "FontAwesomeRegular");
            _ = fonts.AddFont("FontAwesome6FreeSolid.otf", "FontAwesomeSolid");
            _ = fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            _ = fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });
        return builder;
    }
}