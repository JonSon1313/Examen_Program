using DataTransporting;
using DBLayer;
using Models;
using System.Net.Sockets;

namespace Server.Methods
{
    public class AddCommand
    {
        public static void Add<T>(FlyCompanyDBInstance _db, NetworkStream _ns, T _obj)
        {
            var response = new Response();

            if (_obj is Administrator)
            {
                response.Administrators = _db.AddAdministration(_obj as Administrator ?? new());
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as Administrator ?? new()).FirstName} Administrator added ");
            }
            else if (_obj is Aircraft)
            {
                response.Aircrafts = [];
                response.Aircrafts?.Add(_db.AddAircraft(_obj as Aircraft ?? new()));
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as  Aircraft ?? new()).TailNumber} Aircraft added ");
            }
            else if(_obj is Airport)
            {
                response.Airports = [];
                response.Airports?.Add(_db.AddAirport(_obj as Airport ?? new()));
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as Airport ?? new()).FullName} Airport added ");
            }
            else if(_obj is City) 
            {
                response.Cities = [];
                response.Cities?.Add(_db.AddCity(_obj as City ?? new()));
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as City ?? new()).Name} City added ");
            }
            else if(_obj is Client)
            {
                response.Client = _db.AddClient(_obj as Client ?? new());
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as Client ?? new()).FirstName} {(_obj as Client ?? new()).MiddleName} Client added ");
            }
            else if(_obj is Country)
            {
                response.Countries = [];
                response.Countries?.Add(_db.AddCountry(_obj as Country ?? new()));
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as Country ?? new()).Name} Country added ");

            }
            else if(_obj is Flight) 
            {
                response.Flights = [];
                response.Flights?.Add(_db.AddFlight(_obj as Flight ?? new()));
                if(response.Flights?.Count == 1)
                {
                    response.Message = "ADDED";
                }
                Console.WriteLine($"{(_obj as Flight ?? new()).Number} Flight added ");
            }
            else if(_obj is Gate)
            {
                response.Gates = [];
                response.Gates?.Add(_db.AddGate(_obj as Gate ?? new()));
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as Gate ?? new()).Name} Gate added ");
            }
            else if(_obj is SeatType)
            {
                response.SeatTypes = [];
                response.SeatTypes?.Add(_db.AddSeatType(_obj as SeatType ?? new()));
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as SeatType ?? new()).Name} Seat type added ");
            }
            else if(_obj is Terminal)
            {
                response.Terminals = [];
                response.Terminals?.Add(_db.AddTerminal(_obj as Terminal ?? new()));
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as Terminal ?? new()).Name} Terminal added ");
            }
            ByteTransporting.SendBinary(_ns, response); 
        }
    }
}
