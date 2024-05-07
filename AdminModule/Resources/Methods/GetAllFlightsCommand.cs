using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;

namespace AdminModule.Resources.Methods
{
    public class GetAllFlightsCommand
    {
        public static bool Get(List<Flight> _flights, TcpClient _client, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "GETFLIGHTS"
                };

                _client.Connect(_ep);

                using (var ns = _client.GetStream())
                {
                    ByteTransporting.SendBinary(ns, request);

                    Response response = (Response)ByteTransporting.GetBinary(ns);

                    if (response.Message == "PRESENT")
                    {
                        _flights = response.Flights ?? [];
                        return true;
                    }
                    else
                        return false;
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
