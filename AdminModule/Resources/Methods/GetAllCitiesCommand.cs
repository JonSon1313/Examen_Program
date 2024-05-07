using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;

namespace AdminModule.Resources.Methods
{
    public class GetAllCitiesCommand
    {
        public static bool Get(List<City> _cities, TcpClient _client, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "GETCITIES"
                };

                _client.Connect(_ep);

                using (var ns = _client.GetStream())
                {
                    ByteTransporting.SendBinary(ns, request);

                    Response response = (Response)ByteTransporting.GetBinary(ns);

                    if (response.Message == "PRESENT")
                    {
                        _cities = response.Cities ?? [];
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
