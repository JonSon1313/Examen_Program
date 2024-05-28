using DBLayer;
using System.Net.Sockets;
using Models;
using DataTransporting;
namespace Server.Methods
{
    public class GetCommand
    {
        public static void Get(FlyCompanyDBInstance _db, NetworkStream _ns, Request _r)
        {
            var response = new Response();

            if(_r.Message == "GETADMINISTRATOR")
            {
                response.Administrators = _db.GetAdministrator(_r.Administrator.Login, _r.Administrator.Password);
                response.Message = "PRESENT";
                Console.WriteLine($" Administrator has been sended ");
            }
            else if(_r.Message=="GETAIRCRAFT")
            {
                response.Aircrafts = _db.GetAircraft();
                response.Message = "PRESENT";
                Console.WriteLine($" Aircraft has been sended ");
            }
            else if (_r.Message == "GETAIRPORT")
            {
                response.Airports = _db.GetAirport();
                response.Message = "PRESENT";
                Console.WriteLine($" Airport has been sended ");
            }
            else if (_r.Message == "GETCITY")
            {
                response.Cities = _db.GetCity();
                response.Message = "PRESENT";
                Console.WriteLine($" City has been sended ");
            }
            else if (_r.Message == "GETCLIENT")
            {
                response.Client = _db.GetClient(_r.Client.Login,_r.Administrator.Password);
                response.Message = "PRESENT";
                Console.WriteLine($" Client has been sended ");
            }
            else if (_r.Message == "GETCOUNTRY")
            {
                response.Countries = _db.GetCountry();
                response.Message = "PRESENT";
                Console.WriteLine($" Country has been sended ");
            }
            else if (_r.Message == "GETFLIGHT")
            {
                response.Flights = _db.GetFlight();
                response.Message = "PRESENT";
                Console.WriteLine($" Flight has been sended ");
            }
            else if (_r.Message == "GETGATE")
            {
                response.Gates = _db.GetGate();
                response.Message = "PRESENT";
                Console.WriteLine($" Gate has been sended ");
            }
            else if(_r.Message == "GETSEAT")
            {
                response.Seats = _db.GetSeat();
                response.Message = "PRESENT";
                Console.WriteLine($" Seat has been sended ");
            }
            else if(_r.Message == "GETSEATBYID")
            {
                response.Seats = _db.GetSeatById(_r.IdToDelete);
                response.Message = "PRESENT";
                Console.WriteLine($" Seat has been sended ");
            }
            else if (_r.Message == "GETSEATTYPE")
            {
                response.SeatTypes = _db.GetSeatType();
                response.Message = "PRESENT";
                Console.WriteLine($" Seat type has been sended ");
            }
            else if (_r.Message == "GETTERMINAL")
            {
                response.Terminals = _db.GetTerminal();
                response.Message = "PRESENT";
                Console.WriteLine($" Terminal has been sended ");
            }
            else if (_r.Message == "GETTICKET")
            {
                response.Tickets = _db.GetTicketsById(_r.ObjectId);
                response.Message = "PRESENT";
                Console.WriteLine($" Ticket has been sended ");
            }

            ByteTransporting.SendBinary(_ns, response);
        }
    }
}
