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
                response.Tickets?.Add(_db.AddTicket(_r.Ticket[i]) ?? new());
                Console.WriteLine($"{DateTime.Now} --> Ticket - {_r.Ticket[i].Number} has been Successfully added.\r\n");
            }

            if(response.Tickets?.Count == _r?.Ticket?.Count) 
            {
                var user = _db.GetClient(_r!.Ticket[0].ClientId);
                for(int i =0; i < response?.Tickets?.Count; i++)
                {
                    var message = SendToEmailCommand.PrepareEmailBody(user, response.Tickets[i]);
                    SendToEmailCommand.SendEmail(user.Email, "Ticket information", message);
                }
                response.Message = $"SUCCESS";
            }
            else if (response.Tickets!.Count == 0) 
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