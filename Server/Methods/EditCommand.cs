using DBLayer;
using System.Net.Sockets;
using Models;
using DataTransporting;

namespace Server.Methods
{
    public class EditCommand
    {
        public static void Edit<T>(FlyCompanyDBInstance _db, NetworkStream _ns, T _obj)
        {
            var response = new Response();

            if(_obj is Administrator)
            {
                _db.EditAdministration(_obj as Administrator ?? new());
                response.Message = "EDITED";
                Console.WriteLine($"{(_obj as Administrator ?? new()).FirstName} Administrator has been edited ");
            }
            else if (_obj is Aircraft)
            {
                _db.EditAircraft(_obj as Aircraft ?? new());
                response.Message = "EDITED";
                Console.WriteLine($"{(_obj as Aircraft ?? new()).TailNumber} Aircraft has been edited ");
            }
            else if(_obj is Airport)
            {
                _db.EditAirport(_obj as Airport ?? new());
                response.Message = "EDITED";
                Console.WriteLine($"{(_obj as Airport ?? new()).FullName} Airport has been edited ");
            }
            else if(_obj is City)
            {
                _db.EditСity(_obj as City ?? new());
                response.Message = "EDITED";
                Console.WriteLine($"{(_obj as City ?? new()).Name} City has been edited ");
            }
            else if( _obj is Client)
            {
                _db.EditClient(_obj as Client ?? new());
                response.Message = "EDITED";
                Console.WriteLine($"{(_obj as Client ?? new()).FirstName} Client has been edited ");
            }
            else if(_obj is Country)
            {
                _db.EditCountry(_obj as Country ?? new());
                response.Message = "EDITED";
                Console.WriteLine($"{(_obj as Country ?? new()).Name} Country has been edited ");
            }
            else if(_obj is Flight)
            {
                _db.EditFlight(_obj as Flight ?? new());
                response.Message = "EDITED";
                Console.WriteLine($"{(_obj as Flight ?? new()).Number} Flight has been edited ");
            }
            else if(_obj is Gate)
            {
                _db.EditGate(_obj as Gate ?? new());
                response.Message = "EDITED";
                Console.WriteLine($"{(_obj as Gate ?? new()).Name} Gate has been edited");
            }
            else if(_obj is SeatType)
            {
                _db.EditSeatType( _obj as SeatType ?? new());
                response.Message = "EDITED";
                Console.WriteLine($"{(_obj as SeatType ?? new()).Name} Seat type has been edited");
            }
            else if(_obj is Terminal)
            {
                 _db.EditTerminal(_obj as Terminal ?? new());
                response.Message = "EDITED";
                Console.WriteLine($"{(_obj as Terminal ?? new()).Name} Terminal has been edited");
            }
            ByteTransporting.SendBinary(_ns, response);
        }
    }
}