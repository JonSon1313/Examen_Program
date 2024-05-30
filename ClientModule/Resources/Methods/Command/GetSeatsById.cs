using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using System.Collections.ObjectModel;
using ClientModule.Resources.Models;

namespace ClientModule.Resources.Methods
{
    public class GetSeatsById
    {
        public static bool Get<T, C>(T _type, ObservableCollection<C> _object, int id, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = $"GETSEATBYID",
                    IdToDelete = id
                };

                using (var client = new TcpClient())
                {
                    client.Connect(_ep);
                    Response response;
                    using (var ns = client.GetStream())
                    {
                        ByteTransporting.SendBinary(ns, request);

                        response = (Response)ByteTransporting.GetBinary(ns);
                    }
                    if (response.Message == "PRESENT")
                    {
                        for (int i = 0; i < response?.Seats?.Count; i++)
                            (_object as ObservableCollection<SeatNPC>)?
                                .Add(SeatNPC.ConvertFromSeatToNew(response.Seats[i]));

                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
            return false;
        }
    }
}
