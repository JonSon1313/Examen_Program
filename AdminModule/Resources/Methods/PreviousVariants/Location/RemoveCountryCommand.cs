using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using AdminModule.Resources.Repositories;

namespace AdminModule.Resources.Methods
{
    class RemoveCountryCommand
    {
        public static bool Remove(int id, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "REMOVECOUNTRY",
                    IdToDelete = id
                };

                using (var client = new TcpClient())
                {
                    client.Connect(_ep);
                    using (var ns = client.GetStream())
                    {
                        ByteTransporting.SendBinary(ns, request);

                        Response response = (Response)ByteTransporting.GetBinary(ns);

                        if (response.Message == "REMOVED")
                        {
                            WorkingObjectsRepository.Countries?.Remove
                                (WorkingObjectsRepository.Countries.Where(e => e.Id == id)
                                .SingleOrDefault()
                                ?? throw new Exception("Answer in list is missing"));
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
