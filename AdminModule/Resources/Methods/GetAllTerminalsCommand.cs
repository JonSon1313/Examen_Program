using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using AdminModule.Resources.Models;
using System.Collections.ObjectModel;

namespace AdminModule.Resources.Methods
{
    public class GetAllTerminalsCommand
    {
        public static bool Get(ObservableCollection<TerminalNPC> _terminals, IPEndPoint _ep)
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
                            _terminals = [];
                            for (int i = 0; i < response?.Terminals?.Count; i++)
                            {
                                _terminals.Add(TerminalNPC.ConvertFromTerminalToNew(response.Terminals[i]));
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
