using DataTransporting;
using DBLayer;
using Models;
using System.Net.Sockets;

namespace Server.Methods
{
    public class LoginCommand
    {
        public static void LoginAdmin(FlyCompanyDBInstance _db,
            NetworkStream _ns, Request _request)
        {
            string login = _request?.Administrator?.Login ?? "";
            string password = _request?.Administrator?.Password ?? "";
            var response = new Response();

            Salt? dbSalt = _db.GetSalt(login);
            if (dbSalt != null)
            {
                byte[] salt = Convert.FromBase64String(dbSalt.SaltString);

                Administrator? _admin = _db.GetAdministrator(login, DataHashing.Hashing.ToHashSha256WithSalt(salt, password));

                if (_admin != null)
                {
                    response.Message = "LOGGEDIN";
                    response.Administrator = _admin;
                    Console.WriteLine($"{DateTime.Now} --> Admin - {_admin.Login}" +
                        $" Successfully logged in.\r\n");
                }
                else
                {
                    response.Message = "NOTLOGGEDIN";
                    Console.WriteLine($"{DateTime.Now} --> {_request?.Administrator?.Login} Failed attempt to log in!\r\n");
                }
            }
            else
            {
                response.Message = "NOTLOGGEDIN";
                Console.WriteLine($"{DateTime.Now} --> {_request?.Administrator?.Login} Failed attempt to log in!\r\n");
            };
            ByteTransporting.SendBinary(_ns, response);
            _ns.Flush();
        }

        //public static void LoginUser(TestingQuestionDBInstance _db, 
        //    NetworkStream _ns, Request _request)
        //{
        //    var response = new Response();

        //    Salt? dbSalt = _db.GetSalt(_request?.User?.Login ??"");
        //    if (dbSalt != null)
        //    {
        //        byte[] salt = Convert.FromBase64String(dbSalt.SaltString);

        //        User? _user = _db.GetUser(_request?.User?.Login ?? "", 
        //            DataHashing.Hashing.ToHashSha256WithSalt(salt, _request?.User?.Password ?? ""));

        //        if (_user != null)
        //        {
        //            response.Message = "LOGGEDIN";
        //            response.User = _user;
        //            Console.WriteLine($"{DateTime.Now} --> Admin - {_user.Login}" +
        //                $" Successfully logged in.\r\n");
        //        }
        //        else
        //        {
        //            response.Message = "NOTLOGGEDIN";
        //            Console.WriteLine($"{DateTime.Now} --> {_request?.User?.Login} Failed attempt to log in!\r\n");
        //        }
        //    }
        //    else
        //    {
        //        response.Message = "NOTLOGGEDIN";
        //        Console.WriteLine($"{DateTime.Now} --> {_request?.User?.Login} Failed attempt to log in!\r\n");
        //    };
            
        //    ByteTransporting.SendBinary(_ns, response);
        //    _ns.Flush();
        //}
    }
}
