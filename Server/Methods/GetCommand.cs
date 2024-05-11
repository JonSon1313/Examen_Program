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
    public class GetCommand
    {
        public static void Get(FlyCompanyDBInstance _db, NetworkStream _ns, Request _r)
        {
            var response = new Response();
            if(_r.Message=="GETAIRCRAFT")
            {
                response.Aircrafts = _db.GetAircraft();
                response.Message = "PRESENT";
                Console.WriteLine($" Aircraft sended ");
            }

            //окрім Seat
            ByteTransporting.SendBinary(_ns, response);
        }
    }
}
