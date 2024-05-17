using AdminModule.Resources.Repositories;
using DataTransporting;
using Models;
using System.Net;
using System.Net.Sockets;

namespace AdminModule.Resources.Methods
{
    public class LoginCommand
    {
        public static bool Login(Administrator _admin, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "LOGINA",
                    Administrator = _admin
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
                            WorkingObjectsRepository.Admin?.ConvertFromAdministrator(response.Administrators ?? new());
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
