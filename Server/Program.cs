﻿using System.Net.Sockets;
using System.Net;
using static System.Console;
using System.Data;
using DataTransporting;
using DataHashing;
using DBLayer;
using Server.Credentials;
using Models;
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
                                //    stop = true;
                                //    break;
                                //case "SIGNUPA":
                                //    SignupCommand.SignUpAdmin(_db ?? new(), ns, request);
                                //    break;
                                //case "SIGNUPU":
                                //    SignupCommand.SignUpUser(_db ?? new(), ns, request);
                                //    break;
                                //case "LOGINA":
                                //    LoginCommand.LoginAdmin(_db ?? new(), ns, request);
                                //    break;
                                //case "LOGINU":
                                //    LoginCommand.LoginUser(_db ?? new(), ns, request);
                                //    break;
                                
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

