using DataTransporting;
using DBLayer;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
                Console.WriteLine($"{(_obj as Administrator).FirstName} Administrator added ");
            }
            else if (_obj is Aircraft)
            {
                response.Aircrafts = [];
                response.Aircrafts?.Add(_db.AddAircraft(_obj as Aircraft ?? new()));
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as  Aircraft).TailNumber} Aircraft added ");
            }
            else if(_obj is Airport)
            {
                response.Airports = [];
                response.Airports?.Add(_db.AddAirport(_obj as Airport ?? new()));
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as Airport).FullName} Airport added ");
            }
            else if(_obj is City) 
            {
                response.Cities = [];
                response.Cities?.Add(_db.AddCity(_obj as City ?? new()));
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as City).Name} City added ");
            }
            else if (_obj is Client)
            {
                response.Client = _db.AddClient(_obj as Client ?? new());
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as Client).FirstName} Client added ");
            }
            else if(_obj is Country)
            {
                response.Countries = [];
                response.Countries?.Add(_db.AddCountry(_obj as Country ?? new()));
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as Country).Name} Country added ");

            }
            else if(_obj is Flight) 
            {
                response.Flights = [];
                response.Flights?.Add(_db.AddFligt(_obj as Flight ?? new()));
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as Flight).Number} Flight added ");
            }
            else if(_obj is Gate)
            {
                response.Gates = [];
                response.Gates?.Add(_db.AddGate(_obj as Gate ?? new()));
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as Gate).Name} Flight added ");
            }
            else if(_obj is SeatType)
            {
                response.SeatTypes = [];
                response.SeatTypes?.Add(_db.AddSeatType(_obj as SeatType ?? new()));
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as SeatType).Name} Seat type added ");
            }
            else if(_obj is Terminal)
            {
                response.Terminals = [];
                response.Terminals?.Add(_db.AddTerminal(_obj as Terminal ?? new()));
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as Terminal).Name} Terminal added ");
            }
           
            //else if(_obj is Ticket)
            //{
            //    response.Terminals = [];
            //    response.Terminals?.Add(_db.AddTerminal(_obj as Ticket ?? new()));
            //    response.Message = "ADDED";
            //    Console.WriteLine($"{(_obj as Terminal).Name} Ticket added ");
            //}
            //окрім Seat
            ByteTransporting.SendBinary(_ns, response); 

        }

        
    }
}
