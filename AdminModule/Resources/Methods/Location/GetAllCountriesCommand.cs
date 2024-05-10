using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using System.Collections.ObjectModel;
using AdminModule.Resources.Models;

namespace AdminModule.Resources.Methods
{
    public class GetAllCountriesCommand
    {
        public static bool Get(ObservableCollection<CountryNPC> _countries, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "GETCOUNTRIES"
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
                            _countries = [];
                            for (int i = 0; i < response?.Countries?.Count; i++)
                            {
                                _countries.Add(CountryNPC.ConvertFromCountryToNew(response.Countries[i]));
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
