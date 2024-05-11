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

            if (_r.Message =="DELETEAIRCRAFT")
            {
                _db.DeleteAircraft(_r.IdToDelete);
                response.Message = "DELETED";
                Console.WriteLine($" Aircraft {_r.IdToDelete} deleted ");
            }

            //окрім Seat
            ByteTransporting.SendBinary(_ns, response);
        }
    }
}
