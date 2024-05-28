using CommunityToolkit.Mvvm.ComponentModel;
using ClientModule.Resources.Models;
using System.Collections.ObjectModel;
using Models;
using ClientModule.Resources.Repositories;
using ClientModule.Resources.Methods;
using CommunityToolkit.Mvvm.Input;

namespace ClientModule.Resources.ViewModels
{
    public partial class OrderPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string? message;
        [ObservableProperty]
        FlightNPC? flight;
        [ObservableProperty]
        AircraftNPC? aircraft;
        [ObservableProperty]
        ObservableCollection<SeatNPC>? seats;
        [ObservableProperty]
        ObservableCollection<object>? selectedSeats;
        [ObservableProperty]
        ObservableCollection<TicketNPC>? tickets;
        [ObservableProperty]
        TicketNPC? selectedTicket;

        public OrderPageViewModel()
        {
            Flight = WorkingObjectsRepository.TargetFlight;

            Aircraft = WorkingObjectsRepository.Aircrafts?.Where(a => a.Id == Flight!.AircraftId).SingleOrDefault();

            Seats = [];
            GetSeatsById.Get(new Seat(), Seats ?? [], Flight!.Id,
                ConnectionCredentialsRepository.EP ??
                throw new Exception("Endpoint is missing"));
            SelectedSeats = [];
            Tickets = [];
        }

        [RelayCommand]
        private void SelectedSeatsChanged()
        {
            Tickets = [];

            var discount = GetDiscount.Get(Flight.Id, ConnectionCredentialsRepository.EP ?? throw new Exception("Endpoint is missing"));

            for (int i = 0;i < SelectedSeats.Count;i++) 
            {
                Tickets.Add(new()
                {
                    ClientId = WorkingObjectsRepository.Client!.Id,
                    Number = $"{Flight!.Number}-{(SelectedSeats![i] as SeatNPC).Name}",
                    SaleTime = DateTime.Now,
                    FlightId = Flight!.Id,
                    SeatId = Seats[i].Id,
                    discount = discount,
                    finalPrice = flight.BasePrice - (flight.BasePrice / 100 * Convert.ToDecimal(discount))
                });
            }
        }

        [RelayCommand]
        private void SelectedTicketChanged()
        { }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        private void Order()
        {
            if (!OrderTickets.Order(Tickets ?? [], Message ?? "", ConnectionCredentialsRepository.EP
                ?? throw new Exception("Endpoint is missing")))
            { }    
        }
    }
}
