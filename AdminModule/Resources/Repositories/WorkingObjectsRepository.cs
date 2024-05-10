using AdminModule.Resources.Methods;
using AdminModule.Resources.Models;
using System.Collections.ObjectModel;

namespace AdminModule.Resources.Repositories
{
    public static class WorkingObjectsRepository
    {
        public static object? WorkObject { get; set; }

        public static string Action {  get; set; }

        public static AdministratorNPC? Admin { get; set; }
        
        public static ObservableCollection<CityNPC>? Cities { get; set; }
        
        public static ObservableCollection<CountryNPC>? Countries { get; set; }
        
        public static ObservableCollection<AircraftNPC>? Aircrafts { get; set; }
        
        public static ObservableCollection<AirportNPC>? Airports { get; set; }
        
        public static ObservableCollection<GateNPC>? Gates { get; set; }
        
        public static ObservableCollection<TerminalNPC>? Terminals  { get; set; }
        
        public static ObservableCollection<SeatTypeNPC>? SeatTypes { get; set; }
        
        public static ObservableCollection<SeatNPC>? Seats { get; set; }
        
        public static ObservableCollection<FlightNPC>? Flights { get; set; }
        
        public static ObservableCollection<TicketNPC>? Tickets { get; set; }


        static WorkingObjectsRepository()
        {
            Admin = new();
        }

        public static void AdminLoggedIn()
        {
            GetAirplanes();
            GetCities();
            GetCountries();
            GetFlights();
            GetAirport();
            GetGates();
            GetTerminals();
            GetSeats();
            GetSeatTypes();
            GetTickets();
        }

        private static void GetAirplanes()
        {
            if (!GetAllAircraftsCommand.Get(Aircrafts ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            {

            }
        }
        private static void GetCities()
        {
            if (!GetAllCitiesCommand.Get(Cities ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            {

            }
        }
        private static void GetCountries()
        {
            if (!GetAllCountriesCommand.Get(Countries ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            {

            }
        }
        private static void GetAirport()
        {
            if (!GetAllAirportsCommand.Get(Airports ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            {
                
            }
        }
        private static void GetGates()
        {
            if (!GetAllGatesCommand.Get(Gates ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            {

            }
        }
        private static void GetTerminals()
        {
            if (!GetAllTerminalsCommand.Get(Terminals ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            {

            }
        }
        private static void GetSeatTypes()
        {
            if (!GetAllSeatTypesCommand.Get(SeatTypes ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            {
                
            }
        }
        private static void GetSeats()
        {
            if(!GetAllSeatsCommand.Get(Seats ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            {

            }
        }
        private static void GetFlights()
        {
            if (!GetAllFlightsCommand.Get(Flights ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            {

            }
        }
        private static void GetTickets()
        {
            if (!GetAllTicketsCommand.Get(Tickets ?? [],
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing")))
            {

            }
        }
    }
}
