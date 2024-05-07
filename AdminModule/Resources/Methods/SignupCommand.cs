using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;

namespace AdminModule.Resources.Methods
{
    public class SignupCommand
    {
        public static bool Signup(Administrator _admin, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "SIGNUPA",
                    Administrator = _admin
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
                            _admin = response.Administrator
                                ?? throw new Exception("Admin in response is missing");
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
