using AdminModule.Resources.Interfaces;
using AdminModule.Resources.Views;

namespace AdminModule
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddOrEditAircraftPage), typeof(IAddOrEditAircraftPage));
            Routing.RegisterRoute(nameof(SmallObjectAddOrEditLocationAndAirportPage), 
                typeof(ISmallObjectAddOrEditLocationAndAirportPageViewModel));
            Routing.RegisterRoute(nameof(AddOrEditAirportPage), typeof(IAddOrEditAirportPage));
            Routing.RegisterRoute(nameof(AddOrEditFlightPage), typeof(IAddOrEditFlightPage));
        }
    }
}
