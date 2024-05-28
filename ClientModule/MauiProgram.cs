using ClientModule.Resources.Interfaces;
using ClientModule.Resources.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace ClientModule
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
            {
                builder.Services.AddSingleton<ILoginPage, LoginPage>();
                builder.Services.AddSingleton<ISignupPage, SignupPage>();
                builder.Services.AddSingleton<IClientDashboardPage, ClientDashboardPage>();
                builder.Services.AddSingleton<IShopingPage, ShopingPage>();

                builder.Services.AddTransient<IOrderPage, OrderPage>();
            }

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
