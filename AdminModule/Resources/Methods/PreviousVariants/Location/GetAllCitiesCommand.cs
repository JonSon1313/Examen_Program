using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using System.Collections.ObjectModel;
using AdminModule.Resources.Models;

namespace AdminModule.Resources.Methods
{
    public class GetAllCitiesCommand
    {
        public static bool Get(ObservableCollection<CityNPC> _cities, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "GETCITIES"
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
                            _cities = [];
                            for (int i = 0; i < response?.Cities?.Count; i++)
                            {
                                _cities.Add(CityNPC.ConvertFromCityToNew(response.Cities[i]));
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
