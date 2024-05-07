using Admin.Repository;
using Admin.Methods;
using Models;
using System.Net.Sockets;

namespace Admin
{
    internal class Program
    {
        private static Administrator? _admin;
        private static bool _Validated;
        private static bool _loginLoop;

        private static List<City> _cities;
        private static List<Airport> _airports;
        private static List<Aircraft> _aircrafts;
        private static List<Flight> _flights;

        static Program()
        {
            _Validated = false;
            _loginLoop = true;
            _admin = new();

            _cities = [];
            _airports = [];
            _aircrafts = [];
            _flights = [];
        }

        static void Main(string[] args)
        {
            LoginLoopMethod();

            if (_Validated)
            {
                WorkingLoopMethod();
            }
        }

        static void WorkingLoopMethod()
        {
            bool _workingLoop = true;

            using (var client = new TcpClient())
            {
                GetAllCitiesCommand.Get
                 (
                 _cities,
                 client,
                 ConnectionCredentialsRepository.EP ??
                 throw new Exception("EndPoint is Missing"
                ));
            }
            using (var client = new TcpClient())
            {
                GetAllAirportsCommand.Get
                 (
                 _airports,
                 client,
                 ConnectionCredentialsRepository.EP ??
                 throw new Exception("EndPoint is Missing"
                ));
            }
            using (var client = new TcpClient())
            {
                GetAllAircraftsCommand.Get
                (
                _aircrafts,
                client,
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing"
                ));
            }
            using (var client = new TcpClient())
            {
                GetAllFlightsCommand.Get
                (
                _flights,
                client,
                ConnectionCredentialsRepository.EP ??
                throw new Exception("EndPoint is Missing"
                ));
            }

            while (_workingLoop)
            {
                Console.WriteLine("Exit - (X)");

                switch (Console.ReadLine())
                {
                    case "AAP":

                        break;
                    case "X":
                        _workingLoop = false;
                        break;
                }
            }
        }

        static void LoginLoopMethod()
        {
            while (_loginLoop)
            {
                Console.WriteLine("Login as administrator - (L)");
                Console.WriteLine("Signup as administrator - (S)");
                Console.WriteLine("Exit - (X)");

                switch (Console.ReadLine())
                {
                    case "L":
                        using (var client = new TcpClient())
                        {
                            _Validated = LoginCommand.Login
                                (
                                _admin ?? throw new Exception("No Administrator credenials"),
                                client,
                                ConnectionCredentialsRepository.EP 
                                ?? throw new Exception("EndPoint is Missing")
                                );
                        }
                        break;
                    case "S":
                        using (var client = new TcpClient())
                        {
                            _Validated = SignupCommand.Signup
                                (
                                _admin ?? throw new Exception("No Administrator credenials"),
                                client,
                                ConnectionCredentialsRepository.EP 
                                ?? throw new Exception("EndPoint is Missing")
                                );
                        }
                        break;
                    case "X":
                        _loginLoop = false;
                        break;
                }
                if (_Validated)
                    _loginLoop = false;
                else
                    Console.WriteLine("Not logged");
            }
        }
    }
}
