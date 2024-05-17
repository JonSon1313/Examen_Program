using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using AdminModule.Resources.Repositories;
using AdminModule.Resources.Models;

namespace AdminModule.Resources.Methods
{
    class AddCountryCommand
    {
        public static bool Add(Country _country, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "ADDCOUNTRY",
                    Country = _country
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
                            WorkingObjectsRepository.Countries?.Add
                                (CountryNPC.ConvertFromCountryToNew(response.Countries?[0] ?? new()));
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
