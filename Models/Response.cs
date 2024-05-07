
namespace Models
{
    public class Response
    {
        public string Message { get; set; } = null!;

        public List<City>? Cities { get; set; }
        public List<Country>? Countries { get; set; }
        public List<Airport>? Airports { get; set; }
        public List<Aircraft>? Aircrafts { get; set; }
        public List<Flight>? Flights { get; set; }

        public int? TicketId { get; set; }
        public int? TicketNumber { get; set; }

        public Administrator? Administrator { get; set; }
        public Client? Client { get; set; }
    }
}
