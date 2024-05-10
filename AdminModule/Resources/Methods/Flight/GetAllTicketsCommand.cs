using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using AdminModule.Resources.Models;
using System.Collections.ObjectModel;

namespace AdminModule.Resources.Methods
{
    public class GetAllTicketsCommand
    {
        public static bool Get(ObservableCollection<TicketNPC> _tickets, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "GETTICKETS"
                };

                using (var client = new TcpClient())
                {
                    client.Connect(_ep);
                    using (var ns = client.GetStream())
                    {
                        ByteTransporting.SendBinary(ns, request);

                        Response response = (Response)ByteTransporting.GetBinary(ns);

                        if (response.Message == "PRESENT")
                        {
                            _tickets = [];
                            for (int i = 0; i < response?.Tickets?.Count; i++)
                            {
                                _tickets.Add(TicketNPC.ConvertFromTicketToNew(response.Tickets[i]));
                            }
                            return true;
                        }
                        else
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"----> ERROR! Exception : {ex.Message}.");
            }
            return false;
        }
    }
}
