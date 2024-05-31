using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;

namespace AdminModule.Resources.Methods
{
    public class EditCommand
    {
        public static bool Edit<T>(T _object, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = $"EDIT{_object?.GetType().Name.ToUpper()}",
                };

                if (_object is Aircraft)
                    request.Aircraft = _object as Aircraft;
                else if (_object is Airport)
                    request.Airport = _object as Airport;
                else if (_object is Terminal)
                    request.Terminal = _object as Terminal;
                else if (_object is Gate)
                    request.Gate = _object as Gate;
                else if (_object is Country)
                    request.Country = _object as Country;
                else if (_object is City)
                    request.City = _object as City;
                else if (_object is Flight)
                    request.Flight = _object as Flight;
                else if (_object is SeatType)
                    request.SeatType = _object as SeatType;

                using (var client = new TcpClient())
                {
                    client.Connect(_ep);
                    using (var ns = client.GetStream())
                    {
                        ByteTransporting.SendBinary(ns, request);

                        Response response = (Response)ByteTransporting.GetBinary(ns);

                        if (response.Message == "EDITED")
                        {
                            return true;
                        }
                        else
                            return false;
                    }
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
