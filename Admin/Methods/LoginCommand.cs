using DataTransporting;
using Models;
using System.Net;
using System.Net.Sockets;

namespace Admin.Methods
{
    public class LoginCommand
    {
        public static bool Login(Administrator _admin, TcpClient _client, IPEndPoint _ep)
        {
            try
            {
                Console.Write("Enter administrator login - ");
                var login = Console.ReadLine();
                Console.Write("Enter administrator password - ");
                var password = Console.ReadLine();

                Request request = new()
                {
                    Message = "LOGINA",
                    Administrator = new()
                    {
                        Id = -1,
                        FirstName = "",
                        MiddleName = "",
                        LastName = "",
                        Login = login ?? "",
                        Password = password ?? "",
                        PhoneNumber = "",
                        Email = ""
                    }
                };

                _client.Connect(_ep);

                using (var ns  = _client.GetStream())
                {
                    ByteTransporting.SendBinary(ns, request);

                    Response response = (Response)ByteTransporting.GetBinary(ns);

                    if (response.Message == "LOGGEDIN")
                    {
                        _admin = response.Administrator 
                            ?? throw new Exception("Admin in response is missing");
                        return true;
                    }
                    else 
                        return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"----> ERROR! Exception : {ex.Message}.");
            }
            return false;
        }
    }
}
