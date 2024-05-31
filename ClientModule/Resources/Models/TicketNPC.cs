using ClientModule.Resources.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace ClientModule.Resources.Models
{
    public partial class TicketNPC : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string number;
        [ObservableProperty]
        private DateTime saleTime;
        [ObservableProperty]
        private int clientId;
        [ObservableProperty]
        private int flightId;
        [ObservableProperty]
        private int seatId;
        [ObservableProperty]
        public double discount;
        [ObservableProperty]
        public decimal finalPrice;
        [ObservableProperty]
        public string? destination;
        [ObservableProperty]
        public string? starting_place;

        public static TicketNPC ConvertFromTicketToNew(Ticket ticket)
        {
            var Flight = WorkingObjectsRepository.Flights?.Where(i => i.Id == ticket.FlightId).SingleOrDefault() ?? new ();
            return new()
            {
                Id = ticket.Id,
                Number = ticket.Number,
                SaleTime = ticket.SaleTime,
                ClientId = ticket.ClientId,
                FlightId = ticket.FlightId,
                Discount = ticket.Discount,
                FinalPrice = ticket.FinalPrice,
                SeatId = ticket.AtSeat,
                Destination = $"{Flight.ArrivaleCity}. {Flight.ArrivaleCountry}",
                Starting_place = $"{Flight.DepartureCity}. {Flight.DepartureCountry}" 
            };
        }
        public Ticket ConvertToTicket()
        {
            return new()
            {
                Id = this.Id,
                Number = this.Number,
                SaleTime = this.SaleTime,
                ClientId = this.ClientId,
                FlightId = this.FlightId,
                Discount = this.Discount,
                FinalPrice = this.FinalPrice,
                AtSeat = this.SeatId
            };
        }
    }
}