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
    public class AddAirportCommand
    {
        public static bool Add(List<Airport> _airports, List<City> _cities, TcpClient _client, IPEndPoint _ep)
        {
            Console.Clear();
            try
            {
                for (int i = 0; i < _airports.Count; i++)
                    Console.WriteLine($"Id - {_airports[i].Id} Name - {_airports[i].FullName}");
                
                Console.Write("Enter Full name of airport -");
                var fname = Console.ReadLine();

                Console.Write("Enter IATA code - ");
                var iata = Console.ReadLine();

                Console.Write("Enter ICAO code - ");
                var icao = Console.ReadLine();

                for(int i = 0; i < _cities.Count; i++)
                    Console.WriteLine($"Id - {_cities[i].Id} Name - {_cities[i].Name} " +
                        $"Country - {_cities[i].Country.Name}");
                
                Console.Write("Enter CityId - ");
                var cid = Convert.ToInt32(Console.ReadLine());


                Request request = new()
                {
                    Message = "ADDAIRPORT",
                    Airport = new()
                    {
                        Id = -1,
                        FullName = fname ?? "",
                        IATACode = iata ?? "",
                        ICAOCode = icao ?? "",
                        CityId = cid,
                        CountryId = _cities.Where(e => e.Id == cid)
                        .Select(e => e.CountryId).SingleOrDefault()
                    }
                };

                _client.Connect(_ep);

                using (var ns = _client.GetStream())
                {
                    ByteTransporting.SendBinary(ns, request);

                    Response response = (Response)ByteTransporting.GetBinary(ns);

                    if (response.Message == "ADDED")
                    {
                        _airports.Add(response.Airports?[0] ??
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
