
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

        public static TicketNPC ConvertFromTicketToNew(Ticket ticket)
        {
            return new()
            {
                Id = ticket.Id,
                Number = ticket.Number,
                SaleTime = ticket.SaleTime,
                ClientId = ticket.ClientId,
                FlightId = ticket.FlightId,
                Discount = ticket.Discount,
                FinalPrice = ticket.FinalPrice,
                SeatId = ticket.AtSeat
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
