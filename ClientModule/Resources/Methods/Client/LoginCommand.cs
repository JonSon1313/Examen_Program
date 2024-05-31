using ClientModule.Resources.Models;
using ClientModule.Resources.Repositories;
using DataTransporting;
using Models;
using System.Net;
using System.Net.Sockets;

namespace ClientModule.Resources.Methods
{
    public class LoginCommand
    {
        public static bool Login(Client _client, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "LOGINU",
                    Client = _client
                };

                using (var client = new TcpClient())
                {
                    client.Connect(_ep);

                    using (var ns = client.GetStream())
                    {
                        ByteTransporting.SendBinary(ns, request);

                        Response response = (Response)ByteTransporting.GetBinary(ns);

                        if (response.Message == "LOGGEDIN")
                        {
                            WorkingObjectsRepository.Client?.ConvertFromClient(response.Client ?? new());
                            WorkingObjectsRepository.ClientLoggedIn();
                            WorkingObjectsRepository.Client!.Password = "";
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
