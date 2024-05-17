using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using AdminModule.Resources.Repositories;
using AdminModule.Resources.Models;

namespace AdminModule.Resources.Methods
{
    public class AddAircraftCommand
    {
        public static bool Add(Aircraft _aircraft, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "ADDAIRCRAFT",
                    Aircraft = _aircraft
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
                            WorkingObjectsRepository.Aircrafts?.Add
                                (AircraftNPC.ConvertFromAircraftToNew(response.Aircrafts?[0] ?? new()));
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
