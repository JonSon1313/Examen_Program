using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using System.Collections.ObjectModel;
using ClientModule.Resources.Models;

namespace ClientModule.Resources.Methods
{
    public class GetCommand
    {
        public static bool Get<T, C>(T _type, ObservableCollection<C> _object, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = $"GET{_type?.GetType().Name.ToUpper()}"
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
                        if (_type is Aircraft)
                            for (int i = 0; i < response?.Aircrafts?.Count; i++)
                                (_object as ObservableCollection<AircraftNPC>)?
                                    .Add(AircraftNPC.ConvertFromAircraftToNew(response.Aircrafts[i]));
                        else if (_type is Airport)
                            for (int i = 0; i < response?.Airports?.Count; i++)
                                (_object as ObservableCollection<AirportNPC>)?
                                    .Add(AirportNPC.ConvertFromAirportToNew(response.Airports[i]));
                        else if (_type is Terminal)
                            for (int i = 0; i < response?.Terminals?.Count; i++)
                                (_object as ObservableCollection<TerminalNPC>)?
                                    .Add(TerminalNPC.ConvertFromTerminalToNew(response.Terminals[i]));
                        else if (_type is Gate)
                            for (int i = 0; i < response?.Gates?.Count; i++)
                                (_object as ObservableCollection<GateNPC>)?
                                    .Add(GateNPC.ConvertFromGateToNew(response.Gates[i]));
                        else if (_type is Country)
                            for (int i = 0; i < response?.Countries?.Count; i++)
                                (_object as ObservableCollection<CountryNPC>)?
                                    .Add(CountryNPC.ConvertFromCountryToNew(response.Countries[i]));
                        else if (_type is City)
                            for (int i = 0; i < response?.Cities?.Count; i++)
                                (_object as ObservableCollection<CityNPC>)?
                                    .Add(CityNPC.ConvertFromCityToNew(response.Cities[i]));
                        else if (_type is Flight)
                            for (int i = 0; i < response?.Flights?.Count; i++)
                                (_object as ObservableCollection<FlightNPC>)?
                                    .Add(FlightNPC.ConvertFromFlightToNew(response.Flights[i]));
                        else if (_type is Seat)
                            for (int i = 0; i < response?.Seats?.Count; i++)
                                (_object as ObservableCollection<SeatNPC>)?
                                    .Add(SeatNPC.ConvertFromSeatToNew(response.Seats[i]));
                        else if (_type is SeatType)
                            for (int i = 0; i < response?.SeatTypes?.Count; i++)
                                (_object as ObservableCollection<SeatTypeNPC>)?
                                    .Add(SeatTypeNPC.ConvertFromSeatTypeToNew(response.SeatTypes[i]));

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
