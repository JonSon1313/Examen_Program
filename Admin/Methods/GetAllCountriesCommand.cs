using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;

namespace Admin.Methods
{
    public class GetAllCountriesCommand
    {
        public static bool Get(List<Country> _countries, TcpClient _client, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "GETCOUNTRIES"
                };

                _client.Connect(_ep);

                using (var ns = _client.GetStream())
                {
                    ByteTransporting.SendBinary(ns, request);

                    Response response = (Response)ByteTransporting.GetBinary(ns);

                    if (response.Message == "PRESENT")
                    {
                        _countries = response.Countries ?? [];
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
