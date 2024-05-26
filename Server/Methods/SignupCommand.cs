using DataHashing;
using DataTransporting;
using DBLayer;
using Models;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace Server.Methods
{
    public class SignupCommand
    {
        public static void SignUpAdmin(FlyCompanyDBInstance _db,
            NetworkStream _ns, Request _request)
        {
            string login = _request?.Administrator?.Login ?? "";
            Salt? isNotCreated = _db.GetSalt(login);
            var response = new Response();

            if (isNotCreated == null)
            {
                (string, string) saltAndPassword = Hashing.ToHashSha256WithSalt(_request?.Administrator?.Password ?? "");
                _db.AddAdministration(new()
                {
                    Login = login,
                    Password = saltAndPassword.Item1,
                    FirstName = _request?.Administrator?.FirstName ?? "",
                    MiddleName = _request?.Administrator?.MiddleName ?? "",
                    LastName = _request?.Administrator?.LastName ?? "",
                    PhoneNumber = _request?.Administrator?.PhoneNumber ?? "",
                    Email = _request?.Administrator?.Email ?? ""

                     
                });
                _db.AddSalt(new() { Login = login, SaltString = saltAndPassword.Item2 });

                Administrator? newUser = _db.GetAdministrator(login, saltAndPassword.Item1);

                response.Message = "SIGNEDUP";
                response.Administrators = newUser;
                Console.WriteLine($"{DateTime.Now} --> Administrator - {newUser?.Login} has been Successfully created.\r\n");
            }
            else
            {
                response.Message = "NOTSIGNEDUP";
                Console.WriteLine($"{DateTime.Now} --> Administrator was not created!\r\n");
            }
            
            ByteTransporting.SendBinary(_ns, response);
            _ns.Flush();
        }
        public static void SignUpClient(FlyCompanyDBInstance _db,
            NetworkStream _ns, Request _request)
        {
            Client? isNotCreated = _db.GetClient(_request?.Client?.Login ?? "", _request?.Client?.Email ?? "");
            var response = new Response();

            if (isNotCreated == null)
            {
                (string, string) saltAndPassword = Hashing.ToHashSha256WithSalt(_request?.Client?.Password ?? "");
                _request!.Client!.Password = saltAndPassword.Item1;
                _db.AddClient(_request?.Client ?? new());
                _db.AddSalt(new() { Login = _request?.Client?.Login ?? "", SaltString = saltAndPassword.Item2 });

                Client? newUser = _db.GetClient(_request?.Client?.Login ?? "", saltAndPassword.Item1);

                response.Message = "SIGNEDUP";
                response.Client = newUser;
                Console.WriteLine($"{DateTime.Now} --> Client - {newUser?.Login}" +
                    $", {newUser?.Password} Successfully created.\r\n");
            }
            else
            {
                if (isNotCreated?.Login == _request?.Client?.Login)
                    response.Message = "LOGINE";
                else if (isNotCreated?.Email == _request?.Client?.Email)
                    response.Message = "EMAILE";
                else if (isNotCreated?.Login == _request?.Client?.Login &&
                         isNotCreated?.Email == _request?.Client?.Email)
                    response.Message = "BOTHE";

                Console.WriteLine($"{DateTime.Now} --> Client was not created!\r\n");
            }

            ByteTransporting.SendBinary(_ns, response);
            _ns.Flush();
        }
    }
}
