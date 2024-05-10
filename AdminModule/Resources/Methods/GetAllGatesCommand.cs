using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using AdminModule.Resources.Models;
using System.Collections.ObjectModel;

namespace AdminModule.Resources.Methods
{
    public class GetAllGatesCommand
    {
        public static bool Get(ObservableCollection<GateNPC> _gates, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "GETGATES"
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
                            _gates = [];
                            for (int i = 0; i < response?.Gates?.Count; i++)
                            {
                                _gates.Add(GateNPC.ConvertFromGateToNew(response.Gates[i]));
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
