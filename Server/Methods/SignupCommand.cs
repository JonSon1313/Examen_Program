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
                Console.WriteLine($"{DateTime.Now} --> User - {newUser?.Login} has been Successfully created.\r\n");
            }
            else
            {
                response.Message = "NOTSIGNEDUP";
                Console.WriteLine($"{DateTime.Now} --> User was not created!\r\n");
            }
            
            ByteTransporting.SendBinary(_ns, response);
            _ns.Flush();
        }
        //public static void SignUpUser(TestingQuestionDBInstance _db,
        //    NetworkStream _ns, Request _request)
        //{
        //    User? isNotCreated = _db.GetUserForTest(_request?.User?.Login ?? "", _request?.User?.Email ?? "");
        //    var response = new Response();

        //    if (isNotCreated == null)
        //    {
        //        (string, string) saltAndPassword = Hashing.ToHashSha256WithSalt(_request?.User?.Password ?? "");
        //        _request!.User!.Password = saltAndPassword.Item1;
        //        _db.AddUser(_request?.User ?? new());
        //        _db.AddSalt(new() { Login = _request?.User?.Login ?? "", SaltString = saltAndPassword.Item2 });

        //        User? newUser = _db.GetUser(_request?.User?.Login ?? "", saltAndPassword.Item1);

        //        response.Message = "SIGNEDUP";
        //        response.User = newUser;
        //        Console.WriteLine($"{DateTime.Now} --> User - {newUser?.Login}" +
        //            $", {newUser?.Password} Successfully created.\r\n");
        //    }
        //    else
        //    {
        //        if (isNotCreated?.Login == _request?.User?.Login)
        //            response.Message = "LOGINE";
        //        else if (isNotCreated?.Email == _request?.User?.Email)
        //            response.Message = "EMAILE";
        //        else if (isNotCreated?.Login == _request?.User?.Login &&
        //                 isNotCreated?.Email == _request?.User?.Email)
        //            response.Message = "BOTHE";

        //        Console.WriteLine($"{DateTime.Now} --> User was not created!\r\n");
        //    }

        //    ByteTransporting.SendBinary(_ns, response);
        //    _ns.Flush();
        //}
    }
}
