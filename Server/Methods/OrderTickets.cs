using DataTransporting;
using DBLayer;
using Models;
using System.Net.Sockets;

namespace Server.Methods
{
    public class OrderTickets
    {
        public static void Order(FlyCompanyDBInstance _db, NetworkStream _ns, Request _r)
        {
            var response = new Response();

            response.Tickets = [];
            for(int i =0; i < _r?.Ticket?.Count; i++)
            {
                response.Tickets?.Add(_db.AddTicket(_r.Ticket[i]));
            }

            if(response.Tickets?.Count == _r?.Ticket?.Count) 
            {
                response.Message = $"SUCCESS";
            }
            else if (response.Tickets.Count == 0) 
            {
                response.Message = "FAILED";
            }
            else
            {
                response.Message = "PARTIAL";
            }

            ByteTransporting.SendBinary(_ns, response);
        }
    }
}
