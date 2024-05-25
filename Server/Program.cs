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
                                case "SIGNUPU":
                                    SignupCommand.SignUpClient(_db ?? new(), ns, request);
                                break;
                                case "LOGINA":
                                    LoginCommand.LoginAdmin(_db ?? new(), ns, request);
                                    break;
                                case "LOGINU":
                                    LoginCommand.LoginClient(_db ?? new(), ns, request);
                                break;
                                case "ADDAIRCRAFT":
                                    AddCommand.Add(_db ?? new(), ns, request.Aircraft);
                                    break;
                                case "EDITAIRCRAFT":
                                    EditCommand.Edit(_db ?? new(), ns, request.Aircraft);
                                    break;
                                case "DELETEAIRCRAFT":
                                    DeletedCommand.Deleted(_db ?? new(), ns, request);
                                    break;
                                case "GETAIRCRAFT":
                                    GetCommand.Get(_db ?? new(), ns, request);
                                    break;
                                case "ADDAIRPORT":
                                    AddCommand.Add(_db ?? new(), ns, request.Airport);
                                    break;
                                case "EDITAIRPORT":
                                    EditCommand.Edit(_db ?? new(), ns, request.Airport);
                                    break;
                                case "DELETEAIRPORT":
                                    DeletedCommand.Deleted(_db ?? new(), ns, request);
                                    break;
                                case "GETAIRPORT":
                                    GetCommand.Get(_db ?? new(), ns, request);
                                    break;
                                case "ADDCITY":
                                    AddCommand.Add(_db ?? new(), ns, request.City);
                                    break;
                                case "EDITCITY":
                                    EditCommand.Edit(_db ?? new(), ns, request.City);
                                    break;
                                case "DELETECITY":
                                    DeletedCommand.Deleted(_db ?? new(), ns, request);
                                    break;
                                case "GETCITY":
                                    GetCommand.Get(_db ?? new(), ns, request);
                                    break;
                                case "ADDCOUNTRY":
                                    AddCommand.Add(_db ?? new(), ns, request.Country);
                                    break;
                                case "EDITCOUNTRY":
                                    EditCommand.Edit(_db ?? new(), ns, request.Country);
                                    break;
                                case "DELETECOUNTRY":
                                    DeletedCommand.Deleted(_db ?? new(), ns, request);
                                    break;
                                case "GETCOUNTRY":
                                    GetCommand.Get(_db ?? new(), ns, request);
                                    break;
                                case "ADDFLIGHT":
                                    AddCommand.Add(_db ?? new(), ns, request.Flight);
                                    break;
                                case "EDITFLIGHT":
                                    EditCommand.Edit(_db ?? new(), ns, request.Flight);
                                    break;
                                case "DELETEFLIGHT":
                                    DeletedCommand.Deleted(_db ?? new(), ns, request);
                                    break;
                                case "GETFLIGHT":
                                    GetCommand.Get(_db ?? new(), ns, request);
                                    break;
                                case "ADDGATE":
                                    AddCommand.Add(_db ?? new(), ns, request.Gate);
                                    break;
                                case "EDITGATE":
                                    EditCommand.Edit(_db ?? new(), ns, request.Gate);
                                    break;
                                case "DELETEGATE":
                                    DeletedCommand.Deleted(_db ?? new(), ns, request);
                                    break;
                                case "GETGATE":
                                    GetCommand.Get(_db ?? new(), ns, request);
                                    break;
                                case "ADDSEATTYPE":
                                    AddCommand.Add(_db ?? new(), ns, request.SeatType);
                                    break;
                                case "EDITSEATTYPE":
                                    EditCommand.Edit(_db ?? new(), ns, request.SeatType);
                                    break;
                                case "DELETESEATTYPE":
                                    DeletedCommand.Deleted(_db ?? new(), ns, request);
                                    break;
                                case "GETSEATTYPE":
                                    GetCommand.Get(_db ?? new(), ns, request);
                                    break;
                                case "ADDTERMINAL":
                                    AddCommand.Add(_db ?? new(), ns, request.Terminal);
                                    break;
                                case "EDITTERMINAL":
                                    EditCommand.Edit(_db ?? new(), ns, request.Terminal);
                                    break;
                                case "DELETETERMINAL":
                                    DeletedCommand.Deleted(_db ?? new(), ns, request);
                                    break;
                                case "GETTERMINAL":
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

