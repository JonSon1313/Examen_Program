using AdminModule.Resources.Models;
using AdminModule.Resources.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AdminModule.Resources.ViewModels
{
    public partial class AdminDashboardPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<AircraftNPC> aircrafts;
        [ObservableProperty]
        public ObservableCollection<AirportNPC> airports;
        [ObservableProperty]
        public ObservableCollection<FlightNPC> flights;
        public AdminDashboardPageViewModel()
        {
            WorkingObjectsRepository.AdminLoggedIn();
            Aircrafts = WorkingObjectsRepository.Aircrafts ?? [];
            Airports = WorkingObjectsRepository.Airports ?? [];
            Flights = WorkingObjectsRepository.Flights ?? [];
        }
    }
}
