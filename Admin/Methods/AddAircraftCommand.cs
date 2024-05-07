using DataTransporting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Methods
{
    public class AddAircraftCommand
    {
        public static bool Add(List<Aircraft> _aircraft, TcpClient _client, IPEndPoint _ep)
        {
            Console.Clear();
            try
            {
                for (int i = 0; i < _aircraft.Count; i++)
                    Console.WriteLine($"Id - {_aircraft[i].Id} Model - {_aircraft[i].Model}" +
                        $" Tail Number - {_aircraft[i].TailNumber}");

                Console.Write("Enter Manufacturer -");
                var manufacturer = Console.ReadLine();

                Console.Write("Enter Model - ");
                var model = Console.ReadLine();

                Console.Write("Enter Tail number - ");
                var tnumber = Console.ReadLine();

                Request request = new()
                {
                    Message = "ADDAIRCRAFT",
                    Aircraft = new()
                    {
                        Id = -1,
                        Manufacturer = manufacturer ?? "",
                        Model = model ?? "",
                        TailNumber = tnumber ?? ""
                    }
                };

                _client.Connect(_ep);

                using (var ns = _client.GetStream())
                {
                    ByteTransporting.SendBinary(ns, request);

                    Response response = (Response)ByteTransporting.GetBinary(ns);

                    if (response.Message == "ADDED")
                    {
                        _aircraft.Add(response.Aircrafts?[0] ??
                            throw new Exception("Answer in response is missing"));
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
