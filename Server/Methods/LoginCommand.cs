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
                    response.Administrators = _admin;
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

        public static void LoginClient(FlyCompanyDBInstance _db,
            NetworkStream _ns, Request _request)
        {
            var response = new Response();

            Salt? dbSalt = _db.GetSalt(_request?.Client?.Login ?? "");
            if (dbSalt != null)
            {
                byte[] salt = Convert.FromBase64String(dbSalt.SaltString);

                Client? _client = _db.GetClient(_request?.Client?.Login ?? "",
                    DataHashing.Hashing.ToHashSha256WithSalt(salt, _request?.Client?.Password ?? ""));

                if (_client != null)
                {
                    response.Message = "LOGGEDIN";
                    response.Client = _client;
                    Console.WriteLine($"{DateTime.Now} --> Client - {_client.Login}" +
                        $" Successfully logged in.\r\n");
                }
                else
                {
                    response.Message = "NOTLOGGEDIN";
                    Console.WriteLine($"{DateTime.Now} --> {_request?.Client?.Login} Failed attempt to log in!\r\n");
                }
            }
            else
            {
                response.Message = "NOTLOGGEDIN";
                Console.WriteLine($"{DateTime.Now} --> {_request?.Client?.Login} Failed attempt to log in!\r\n");
            };

            ByteTransporting.SendBinary(_ns, response);
            _ns.Flush();
        }
    }
}
