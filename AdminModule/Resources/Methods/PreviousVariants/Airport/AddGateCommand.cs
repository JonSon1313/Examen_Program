using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using AdminModule.Resources.Repositories;
using AdminModule.Resources.Models;

namespace AdminModule.Resources.Methods
{
    public class AddGateCommand
    {
        public static bool Add(Gate _gate, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "ADDGATE",
                    Gate = _gate
                };

                using (var client = new TcpClient())
                {
                    client.Connect(_ep);
                    using (var ns = client.GetStream())
                    {
                        ByteTransporting.SendBinary(ns, request);

                        Response response = (Response)ByteTransporting.GetBinary(ns);

                        if (response.Message == "ADDED")
                        {
                            WorkingObjectsRepository.Gates?.Add
                                (GateNPC.ConvertFromGateToNew(response.Gates?[0] ?? new()));
                            return true;
                        }
                        else
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
