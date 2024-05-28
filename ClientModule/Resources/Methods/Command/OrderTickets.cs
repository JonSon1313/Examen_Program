using ClientModule.Resources.Models;
using ClientModule.Resources.Repositories;
using DataTransporting;
using Models;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;

namespace ClientModule.Resources.Methods
{
    public class OrderTickets
    {
        public static bool Order(ObservableCollection<TicketNPC> tickets, string Message, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = $"ORDERTICKET",
                };
                request.Ticket = [];
                for (int i = 0; i < tickets.Count; i++)
                {
                    request.Ticket.Add(tickets[i].ConvertToTicket());
                }

                using (var client = new TcpClient())
                {
                    client.Connect(_ep);
                    Response response;
                    using (var ns = client.GetStream())
                    {
                        ByteTransporting.SendBinary(ns, request);

                        response = (Response)ByteTransporting.GetBinary(ns);
                    }
                    if (response.Message == "SUCCESS" || response.Message == "PARTIAL")
                    {
                        for (int i = 0; i < response.Tickets?.Count; i++)
                            WorkingObjectsRepository.Tickets?.Add(TicketNPC.ConvertFromTicketToNew(response.Tickets[i]));
                        if (response.Message == "PARTIAL")
                            Message = "Some of Tickets was not bought";
                        return true;
                    }
                    if (response.Message == "FAILED")
                    {
                        Message = "None of tickets has been bought";
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
