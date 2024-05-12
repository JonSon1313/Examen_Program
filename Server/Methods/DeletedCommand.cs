using DBLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Models;
using DataTransporting;
namespace Server.Methods
{
    public  class DeletedCommand
    {
        public static void Deleted(FlyCompanyDBInstance _db, NetworkStream _ns, Request _r)
        {
            var response = new Response();

            if(_r.Message == "DELETEADMINISTRATOR")
            {
                _db.DeleteAdministration(_r.IdToDelete);
                response.Message = "DELETED";
                Console.WriteLine($" Administrator {_r.IdToDelete} deleted ");
            }
            else if (_r.Message =="DELETEAIRCRAFT")
            {
                _db.DeleteAircraft(_r.IdToDelete);
                response.Message = "DELETED";
                Console.WriteLine($" Aircraft {_r.IdToDelete} deleted ");
            }
            else if (_r.Message == "DELETEAIRPORT")
            {
                _db.DeleteAirport(_r.IdToDelete);
                response.Message = "DELETED";
                Console.WriteLine($" Airport {_r.IdToDelete} deleted ");
            }
            else if (_r.Message == "DELETECITY")
            {
                _db.DeleteCity(_r.IdToDelete);
                response.Message = "DELETED";
                Console.WriteLine($" City {_r.IdToDelete} deleted ");
            }
            else if (_r.Message == "DELETECLIENT")
            {
                _db.DeleteClient(_r.IdToDelete);
                response.Message = "DELETED";
                Console.WriteLine($" Client {_r.IdToDelete} deleted ");
            }
            else if (_r.Message == "DELETECOUNTRY")
            {
                _db.DeleteCountry(_r.IdToDelete);
                response.Message = "DELETED";
                Console.WriteLine($" Country {_r.IdToDelete} deleted ");
            }
            else if (_r.Message == "DELETEFLIGHT")
            {
                _db.DeleteFlight(_r.IdToDelete);
                response.Message = "DELETED";
                Console.WriteLine($" Flight {_r.IdToDelete} deleted ");
            }
            else if (_r.Message == "DELETEGATE")
            {
                _db.DeleteGate(_r.IdToDelete);
                response.Message = "DELETED";
                Console.WriteLine($" Gate {_r.IdToDelete} deleted ");
            }
            else if (_r.Message == "DELETESEATTYPE")
            {
                _db.DeleteSeatType(_r.IdToDelete);
                response.Message = "DELETED";
                Console.WriteLine($" Seat type {_r.IdToDelete} deleted ");
            }
            else if (_r.Message == "DELETETERMINAL")
            {
                _db.DeleteTerminal(_r.IdToDelete);
                response.Message = "DELETED";
                Console.WriteLine($" Terminal {_r.IdToDelete} deleted ");
            }
             //окрім Seat
            ByteTransporting.SendBinary(_ns, response);
        }
    }
}
