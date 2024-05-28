using DataTransporting;
using DBLayer;
using System.Net.Sockets;
using Models;

namespace Server.Methods
{
   public class RemoveTickets
    {
        public static void Remove(FlyCompanyDBInstance _db, NetworkStream _ns, Request _r)
        {
            var response = new Response();

            _db.DeleteTicket(_r.Ticket[0]);
            response.Message = "PARTIAL";
            ByteTransporting.SendBinary(_ns, response);
            Console.WriteLine($"{DateTime.Now} --> Ticket - {_r.Ticket[0].Number} has been Successfully deleted.\r\n");


        }
    }
}
