using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using System.Collections.ObjectModel;
using AdminModule.Resources.Models;

namespace AdminModule.Resources.Methods
{
    public class GetAllAircraftsCommand
    {
        public static bool Get(ObservableCollection<AircraftNPC> _aircraft, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "GETAIRCRAFTS"
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
                            _aircraft = [];
                            for (int i = 0; i < response?.Aircrafts?.Count; i++)
                            {
                                _aircraft.Add(AircraftNPC.ConvertFromAircraftToNew(response.Aircrafts[i]));
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
                Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
            return false;
        }
    }
}
