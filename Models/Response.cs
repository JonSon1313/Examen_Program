
namespace Models
{
    [Serializable]
    public class Response
    {
        public string Message { get; set; } = null!;

        public List<City>? Cities { get; set; }
        public List<Country>? Countries { get; set; }
        public List<Airport>? Airports { get; set; }

        public List<Gate>? Gates { get; set; }
        public List<Terminal>? Terminals { get; set; }

        public List<Aircraft>? Aircrafts { get; set; }
        public List<Flight>? Flights { get; set; }
        public List<Ticket>? Tickets { get; set; }

        public List<SeatType>? SeatTypes { get; set; }
        public List<Seat>? Seats { get; set; }

        public int? TicketId { get; set; }
        public int? TicketNumber { get; set; }

        public double? Discount { get; set; }

        public Administrator? Administrators { get; set; }
        public Client? Client { get; set; }

        
    }
}
