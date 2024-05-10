using AdminModule.Resources.Interfaces;
using AdminModule.Resources.ViewModels;
using AdminModule.Resources.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace AdminModule
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
                builder.Services.AddTransient<LoginPageViewModel>();
                builder.Services.AddSingleton<ISignupPage, SignupPage>();
                builder.Services.AddSingleton<IAdminDashboardPage, AdminDashboardPage>();
            }

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
