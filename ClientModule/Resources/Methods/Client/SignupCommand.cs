using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using ClientModule.Resources.Repositories;

namespace ClientModule.Resources.Methods
{
    public class SignupCommand
    {
        public static bool Signup(Client _client, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "SIGNUPU",
                    Client = _client
                };

                using (var client = new TcpClient())
                {
                    client.Connect(_ep);

                    using (var ns = client.GetStream())
                    {
                        ByteTransporting.SendBinary(ns, request);

                        Response response = (Response)ByteTransporting.GetBinary(ns);

                        if (response.Message == "SIGNEDUP")
                        {
                            WorkingObjectsRepository.Client?.ConvertFromClient(response.Client ?? new());
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
