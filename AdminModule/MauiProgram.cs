using AdminModule.Resources.Interfaces;
using AdminModule.Resources.Views;
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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<ILoginPage, LoginPage>();
            builder.Services.AddSingleton<ISignupPage, SignupPage>();
            builder.Services.AddSingleton<IAdminDashboardPage, AdminDashboardPage>();
            builder.Services.AddSingleton<IAircraftPage, AircraftPage>();
            builder.Services.AddSingleton<ILocationAndAirportPage, LocationAndAirportPage>();

            builder.Services.AddTransient<IAddOrEditAircraftPage, AddOrEditAircraftPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
