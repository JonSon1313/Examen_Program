using CommunityToolkit.Mvvm.ComponentModel;
using ClientModule.Resources.Models;
using System.Collections.ObjectModel;
using Models;
using ClientModule.Resources.Repositories;
using ClientModule.Resources.Methods;

namespace ClientModule.Resources.ViewModels
{
    public partial class OrderPageViewModel : ObservableObject
    {
        [ObservableProperty]
        FlightNPC? flight;
        [ObservableProperty]
        AircraftNPC? aircraft;
        [ObservableProperty]
        ObservableCollection<SeatNPC>? seats;
        [ObservableProperty]
        ObservableCollection<TicketNPC>? tickets;

        public OrderPageViewModel()
        {
            Flight = WorkingObjectsRepository.TargetFlight;

            Aircraft = WorkingObjectsRepository.Aircrafts.Where(a => a.Id == Flight.AircraftId).SingleOrDefault();

            GetSeatsById.Get(new Seat(), Seats ?? [], Flight.Id,
                ConnectionCredentialsRepository.EP ??
                throw new Exception("Endpoint is missing"));
        }
    }
}
