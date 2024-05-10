using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using System.Collections.ObjectModel;
using AdminModule.Resources.Models;

namespace AdminModule.Resources.Methods
{
    public class GetAllAirportsCommand
    {
        public static bool Get(ObservableCollection<AirportNPC> _airports, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "GETAIRPORTS"
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
                            _airports = [];
                            for (int i = 0; i < response?.Airports?.Count; i++)
                            {
                                _airports.Add(AirportNPC.ConvertFromAirportToNew(response.Airports[i]));
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
