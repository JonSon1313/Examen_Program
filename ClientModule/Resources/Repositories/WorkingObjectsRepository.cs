using ClientModule.Resources.Methods;
using ClientModule.Resources.Models;
using System.Collections.ObjectModel;
using Models;

namespace ClientModule.Resources.Repositories
{
    public static class WorkingObjectsRepository
    {
        public static ClientNPC? Client {  get; set; }
        public static ObservableCollection<CityNPC>? Cities { get; set; }
        public static ObservableCollection<CountryNPC>? Countries { get; set; }
        public static ObservableCollection<AircraftNPC>? Aircrafts { get; set; }
        public static ObservableCollection<AirportNPC>? Airports { get; set; }
        public static ObservableCollection<GateNPC>? Gates { get; set; }
        public static ObservableCollection<TerminalNPC>? Terminals { get; set; }
        public static ObservableCollection<SeatTypeNPC>? SeatTypes { get; set; }
        public static ObservableCollection<SeatNPC>? Seats { get; set; }
        public static ObservableCollection<FlightNPC>? Flights { get; set; }
        public static ObservableCollection<TicketNPC>? Tickets { get; set; }


        static WorkingObjectsRepository()
        {
            Client = new();
            Cities = [];
            Countries = [];
            Aircrafts = [];
            Airports = [];
            Gates = [];
            Terminals = [];
            Seats = [];
            Tickets = [];
            Flights = [];
            SeatTypes = [];
        }

        public static void ClientLoggedIn()
        {
            GetAirplanes();
            GetCities();
            GetCountries();
            GetAirport();
            GetGates();
            GetTerminals();
            //GetSeats();
            GetSeatTypes();
            //GetTickets();
            GetFlights();
        }

        public static void ClientLogoff()
        {
            Client = new();
            Aircrafts = null;
            Airports = null;
            Terminals = null;
            Seats = null;
            Tickets = null;
            Gates = null;
            Cities = null;
            Countries = null;
            Flights = null;
        }

        private static void GetAirplanes()
        {
            if (!GetCommand.Get(new Aircraft(), Aircrafts ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            { }
        }
        private static void GetCities()
        {
            if (!GetCommand.Get(new City(), Cities ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            { }
        }
        private static void GetCountries()
        {
            if (!GetCommand.Get(new Country(), Countries ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            { }
        }
        private static void GetAirport()
        {
            if (!GetCommand.Get(new Airport(), Airports ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            { }
        }
        private static void GetGates()
        {
            if (!GetCommand.Get(new Gate(), Gates ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            { }
        }
        private static void GetTerminals()
        {
            if (!GetCommand.Get(new Terminal(), Terminals ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            { }
        }
        private static void GetSeatTypes()
        {
            if (!GetCommand.Get(new SeatType(), SeatTypes ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            { }
        }
        private static void GetSeats()
        {
            if (!GetCommand.Get(new Seat(), Seats ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            { }
        }
        private static void GetFlights()
        {
            if (!GetCommand.Get(new Flight(), Flights ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            { }
        }
        private static void GetTickets()
        {
            if (!GetCommand.Get(new Ticket(), Tickets ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            { }
        }
    }
}
