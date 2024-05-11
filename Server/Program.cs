using System.Net.Sockets;
using System.Net;
using static System.Console;
using System.Data;
using DataTransporting;
using DataHashing;
using DBLayer;
using Server.Credentials;
using Models;
using Server.Methods;
namespace Server
{
    internal class Program
    {
        private static TcpListener? _listener;
        private static FlyCompanyDBInstance? _db;
        static async Task Main(string[] args)
        {
            _db = new();
            _listener = new(ConnectionCredentialsRepository.EP ??
                new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10221));

            _listener.Start();
            WriteLine($"{DateTime.Now} -> SERVER HAS BEEN STARTED.\r\n");

            await WorkingLoop();
        }
        private static async Task WorkingLoop()
        {
            bool stop = false;
            while (!stop)
            {
                try
                {
                    using (var acceptor = await _listener?.AcceptTcpClientAsync()!)
                    {
                        using (var ns = acceptor.GetStream())
                        {
                            Request request = (Request)ByteTransporting.GetBinary(ns);

                            switch (request.Message)
                            {
                                case "STOPSERVER":
                                    stop = true;
                                    break;
                                case "SIGNUPA":
                                    SignupCommand.SignUpAdmin(_db ?? new(), ns, request);
                                    break;
                                //case "SIGNUPU":
                                //    SignupCommand.SignUpUser(_db ?? new(), ns, request);
                                //break;
                                case "LOGINA":
                                    LoginCommand.LoginAdmin(_db ?? new(), ns, request);
                                    break;
                                //case "LOGINU":
                                //    LoginCommand.LoginUser(_db ?? new(), ns, request);
                                //break;
                                case "ADDAIRCRAFT":
                                    AddCommand.Add(_db ?? new(), ns, request.Aircraft);
                                    break;
                                case "EDITAIRCRAFT":
                                    EditCommand.Edit(_db ?? new(), ns, request.Aircraft);
                                    break;
                                case "DELETEAIRCRAFT":
                                    DeletedCommand.Deleted(_db ?? new(), ns, request);
                                    break;
                                case "GETAIRCARFT":
                                    GetCommand.Get(_db ?? new(), ns, request);
                                    break;

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    WriteLine($"{DateTime.Now} -> !! ERROR - {ex}.\r\n");
                }
            }
        }
    }
}

