using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using AdminModule.Resources.Repositories;
using AdminModule.Resources.Models;

namespace AdminModule.Resources.Methods
{
    public class AddCommand
    {
        public static bool Add<T> (T _object, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = $"ADD{_object?.GetType().Name.ToUpper()}",
                };

                if (_object is Aircraft)
                    request.Aircraft = _object as Aircraft;
                else if(_object is Airport)
                    request.Airport = _object as Airport;
                else if(_object is Terminal)
                    request.Terminal = _object as Terminal;
                else if(_object is Gate)
                    request.Gate = _object as Gate;
                else if(_object is Country)
                    request.Country = _object as Country;
                else if( _object is City)
                    request.City = _object as City;
                else if(_object is Flight)
                    request.Flight = _object as Flight;
                else if(_object is SeatType)
                    request.SeatType = _object as SeatType;

                using (var client = new TcpClient())
                {
                    client.Connect(_ep);

                    Response response;
                    using (var ns = client.GetStream())
                    {
                        ByteTransporting.SendBinary(ns, request);

                        response = (Response)ByteTransporting.GetBinary(ns);
                    }
                    if (response.Message == "ADDED")
                    {
                        if (_object is Aircraft)
                            WorkingObjectsRepository.Aircrafts?.Add
                            (AircraftNPC.ConvertFromAircraftToNew(response.Aircrafts?[0] ?? new()));
                        else if (_object is Airport)
                            WorkingObjectsRepository.Airports?.Add
                                (AirportNPC.ConvertFromAirportToNew(response.Airports?[0] ?? new()));
                        else if (_object is Terminal)
                            WorkingObjectsRepository.Terminals?.Add
                                (TerminalNPC.ConvertFromTerminalToNew(response.Terminals?[0] ?? new()));
                        else if (_object is Gate)
                            WorkingObjectsRepository.Gates?.Add
                                (GateNPC.ConvertFromGateToNew(response.Gates?[0] ?? new()));
                        else if (_object is Country)
                            WorkingObjectsRepository.Countries?.Add
                                (CountryNPC.ConvertFromCountryToNew(response.Countries?[0] ?? new()));
                        else if (_object is City)
                            WorkingObjectsRepository.Cities?.Add
                                (CityNPC.ConvertFromCityToNew(response.Cities?[0] ?? new()));
                        else if (_object is Flight)
                            WorkingObjectsRepository.Flights?.Add
                                (FlightNPC.ConvertFromFlightToNew(response.Flights?[0] ?? new()));
                        else if (_object is SeatType)
                            WorkingObjectsRepository.SeatTypes?.Add
                                (SeatTypeNPC.ConvertFromSeatTypeToNew(response.SeatTypes?[0] ?? new()));

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
