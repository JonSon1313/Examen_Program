using System.Net;
using System.Net.Sockets;
using ClientModule.Resources.Repositories;
using ClientModule.Resources.Models;
using DataTransporting;
using Models;

namespace ClientModule.Resources.Methods.Command
{
    public class ReturnTicket
    {
        public static bool Return(TicketNPC tickets, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = $"RETURNTICKET",
                };
                request.Ticket = [];
                request.Ticket.Add(tickets.ConvertToTicket());
                

                using (var client = new TcpClient())
                {
                    client.Connect(_ep);
                    Response response;
                    using (var ns = client.GetStream())
                    {
                        ByteTransporting.SendBinary(ns, request);

                        response = (Response)ByteTransporting.GetBinary(ns);
                    }
                    if (response.Message == "SUCCESS")
                    {
                        WorkingObjectsRepository.Tickets?.Remove(tickets);
                        return true;
                    }
                    if (response.Message == "FAILED")
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }

            return false;
        }
    }
}
