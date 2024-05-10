using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using AdminModule.Resources.Models;
using System.Collections.ObjectModel;

namespace AdminModule.Resources.Methods
{
    public class GetAllFlightsCommand
    {
        public static bool Get(ObservableCollection<FlightNPC> _flights, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "GETFLIGHTS"
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
                            _flights = [];
                            for (int i = 0; i < response?.Flights?.Count; i++)
                            {
                                _flights.Add(FlightNPC.ConvertFromFlightToNew(response.Flights[i]));
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
