using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;

namespace Admin.Methods
{
    public class GetAllAircraftsCommand
    {
        public static bool Get(List<Aircraft> _aircraft, TcpClient _client, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "GETAIRCRAFTS"
                };

                _client.Connect(_ep);

                using (var ns = _client.GetStream())
                {
                    ByteTransporting.SendBinary(ns, request);

                    Response response = (Response)ByteTransporting.GetBinary(ns);

                    if (response.Message == "PRESENT")
                    {
                        _aircraft = response.Aircrafts ?? [];
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
