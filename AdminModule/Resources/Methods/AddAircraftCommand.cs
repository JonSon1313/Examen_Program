using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;

namespace AdminModule.Resources.Methods
{
    public class AddAircraftCommand
    {
        public static bool Add(Aircraft _aircraft ,List<Aircraft> _aircrafts, TcpClient _client, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "ADDAIRCRAFT",
                    Aircraft = _aircraft
                };

                _client.Connect(_ep);

                using (var ns = _client.GetStream())
                {
                    ByteTransporting.SendBinary(ns, request);

                    Response response = (Response)ByteTransporting.GetBinary(ns);

                    if (response.Message == "ADDED")
                    {
                        _aircrafts.Add(response.Aircrafts?[0] ??
                            throw new Exception("Answer in response is missing"));
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
