using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using AdminModule.Resources.Models;
using System.Collections.ObjectModel;

namespace AdminModule.Resources.Methods
{
    public class GetAllSeatsCommand
    {
        public static bool Get(ObservableCollection<SeatNPC> _seats, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "GETSEATS"
                };

                using (var client = new TcpClient())
                {
                    client.Connect(_ep);
                    using (var ns = client.GetStream())
                    {
                        ByteTransporting.SendBinary(ns, request);

                        Response response = (Response)ByteTransporting.GetBinary(ns);

                        if (response.Message == "PRESENT")
                        {
                            _seats = [];
                            for (int i = 0; i < response?.Seats?.Count; i++)
                            {
                                _seats.Add(SeatNPC.ConvertFromSeatToNew(response.Seats[i]));
                            }
                            return true;
                        }
                        else
                            return false;
                    }
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
