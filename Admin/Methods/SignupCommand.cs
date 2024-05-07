using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;

namespace Admin.Methods
{
    public class SignupCommand
    {
        public static bool Signup(Administrator _admin, TcpClient _client, IPEndPoint _ep)
        {
            try
            {
                Console.Write("Enter new administrator login - ");
                var login = Console.ReadLine();
                Console.Write("Enter new administrator password - ");
                var password = Console.ReadLine();
                Console.Write("Enter first name - ");
                var fname = Console.ReadLine();
                Console.Write("Enter middle name - ");
                var mname = Console.ReadLine();
                Console.Write("Enter last name - ");
                var lname = Console.ReadLine();
                Console.Write("Enter your Email - ");
                var email = Console.ReadLine();
                Console.WriteLine("Enter your phone number - ");
                var phone = Console.ReadLine();

                Request request = new()
                {
                    Message = "SIGNUPA",
                    Administrator = new()
                    {
                        Id = -1,
                        FirstName = fname ?? "",
                        MiddleName = mname ?? "",
                        LastName = lname ?? "",
                        Login = login ?? "",
                        Password = password ?? "",
                        PhoneNumber = phone ?? "",
                        Email = email ?? ""
                    }
                };

                _client.Connect(_ep);

                using (var ns = _client.GetStream())
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
            catch (Exception ex)
            {
                Console.WriteLine($"----> ERROR! Exception : {ex.Message}.");
            }
            return false;
        }
    }
}
