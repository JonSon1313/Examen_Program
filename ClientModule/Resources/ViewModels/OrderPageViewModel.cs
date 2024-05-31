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
        [NotifyCanExecuteChangedFor(nameof(OrderCommand))]
        ObservableCollection<TicketNPC>? tickets;
        [ObservableProperty]
        TicketNPC? selectedTicket;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(OrderCommand))]
        public string? cardNumber;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(OrderCommand))]
        public string? cardsHolderFullName;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(OrderCommand))]
        public string? cardcvv;


        public OrderPageViewModel()
        {
            Cardcvv = "";
            CardsHolderFullName = "";
            CardNumber = "";

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

            var discount = GetDiscount.Get(Flight!.Id, ConnectionCredentialsRepository.EP ?? throw new Exception("Endpoint is missing"));

            for (int i = 0;i < SelectedSeats?.Count;i++) 
            {
                if ((selectedSeats[i] as SeatNPC ?? new()).Reserved == false)
                {
                    Tickets.Add(new()
                    {
                        ClientId = WorkingObjectsRepository.Client!.Id,
                        Number = $"{Flight!.Number}-{(SelectedSeats[i] as SeatNPC)?.Name}",
                        SaleTime = DateTime.Now,
                        FlightId = Flight!.Id,
                        SeatId = (SelectedSeats[i] as SeatNPC ?? new()).Id,
                        Discount = discount,
                        FinalPrice = Flight.BasePrice - (Flight.BasePrice / 100 * Convert.ToDecimal(discount))
                    });
                }
            }

            OrderCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand]
        private void SelectedTicketChanged()
        { }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand(CanExecute = nameof(CanExecute))]
        private void Order()
        {
            if (OrderTickets.Order(Tickets ?? [], Message ?? "", ConnectionCredentialsRepository.EP
                ?? throw new Exception("Endpoint is missing")))
            {
                Cardcvv = "";
                CardsHolderFullName = "";
                CardNumber = "";

                Seats = [];
                GetSeatsById.Get(new Seat(), Seats ?? [], Flight!.Id,
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("Endpoint is missing"));

                Tickets = [];
                SelectedTicket = new();

                Shell.Current.DisplayAlert("Success", "Tickets have been successfully purchased", "Ok");
            }    
        }
        private bool CanExecute()
        {
            return CheckCreditCardCVV.Check(Cardcvv ?? "")
                && CheckCreditCardNumber.Check(CardNumber ?? "")
                && CheckCreditCardOwnerName.Check(CardsHolderFullName?? "")
                && Tickets?.Count > 0;
        }
    }
}