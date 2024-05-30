using System.Net.Sockets;
using System.Net;
using static System.Console;
using DataTransporting;
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
                                case "GETDISCOUNT":
                                    var response = new Response();
                                    response.Discount = _db?.GetDiscountForTicket(request.ObjectId, request.Date);
                                    response.Message = "PRESENT";
                                    ByteTransporting.SendBinary(ns, response);
                                    break;
                                case "ORDERTICKET":
                                    OrderTickets.Order(_db ?? new(), ns, request);
                                    break;
                                case "RETURNTICKET":
                                    RemoveTickets.Remove(_db ?? new(), ns, request);
                                    break;
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
                                case "DELETEAIRCRAFT":
                                case "DELETEAIRPORT":
                                case "DELETECITY":
                                case "DELETECOUNTRY":
                                case "DELETEFLIGHT":
                                case "DELETEGATE":
                                case "DELETESEATTYPE":
                                case "DELETETERMINAL":
                                    DeletedCommand.Deleted(_db ?? new(), ns, request);
                                    break;
                                case "GETAIRCRAFT":
                                case "GETAIRPORT":
                                case "GETCITY":
                                case "GETCOUNTRY":
                                case "GETFLIGHT":
                                case "GETGATE":
                                case "GETSEAT":
                                case "GETSEATBYID":
                                case "GETSEATTYPE":
                                case "GETTICKET":
                                case "GETTERMINAL":
                                    GetCommand.Get(_db ?? new(), ns, request);
                                    break;
                                case "ADDAIRCRAFT":
                                    AddCommand.Add(_db ?? new(), ns, request.Aircraft);
                                    break;
                                case "EDITAIRCRAFT":
                                    EditCommand.Edit(_db ?? new(), ns, request.Aircraft);
                                    break;
                                case "ADDAIRPORT":
                                    AddCommand.Add(_db ?? new(), ns, request.Airport);
                                    break;
                                case "EDITAIRPORT":
                                    EditCommand.Edit(_db ?? new(), ns, request.Airport);
                                    break;
                                case "ADDCITY":
                                    AddCommand.Add(_db ?? new(), ns, request.City);
                                    break;
                                case "EDITCITY":
                                    EditCommand.Edit(_db ?? new(), ns, request.City);
                                    break;
                                case "ADDCOUNTRY":
                                    AddCommand.Add(_db ?? new(), ns, request.Country);
                                    break;
                                case "EDITCOUNTRY":
                                    EditCommand.Edit(_db ?? new(), ns, request.Country);
                                    break;
                                case "ADDFLIGHT":
                                    AddCommand.Add(_db ?? new(), ns, request.Flight);
                                    break;
                                case "EDITFLIGHT":
                                    EditCommand.Edit(_db ?? new(), ns, request.Flight);
                                    break;
                                case "ADDGATE":
                                    AddCommand.Add(_db ?? new(), ns, request.Gate);
                                    break;
                                case "EDITGATE":
                                    EditCommand.Edit(_db ?? new(), ns, request.Gate);
                                    break;
                                case "ADDSEATTYPE":
                                    AddCommand.Add(_db ?? new(), ns, request.SeatType);
                                    break;
                                case "EDITSEATTYPE":
                                    EditCommand.Edit(_db ?? new(), ns, request.SeatType);
                                    break;
                                case "ADDTERMINAL":
                                    AddCommand.Add(_db ?? new(), ns, request.Terminal);
                                    break;
                                case "EDITTERMINAL":
                                    EditCommand.Edit(_db ?? new(), ns, request.Terminal);
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