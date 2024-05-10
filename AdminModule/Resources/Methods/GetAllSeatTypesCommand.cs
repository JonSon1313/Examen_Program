using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using AdminModule.Resources.Models;
using System.Collections.ObjectModel;

namespace AdminModule.Resources.Methods
{
    public class GetAllSeatTypesCommand
    {
        public static bool Get(ObservableCollection<SeatTypeNPC> _seatTypes, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "GETSEATTYPES"
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
                            _seatTypes = [];
                            for (int i = 0; i < response?.SeatTypes?.Count; i++)
                            {
                                _seatTypes.Add(SeatTypeNPC.ConvertFromSeatTypeToNew(response.SeatTypes[i]));
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
