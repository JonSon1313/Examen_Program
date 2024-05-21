using DataTransporting;
using Models;
using System.Net.Sockets;
using System.Net;
using AdminModule.Resources.Repositories;

namespace AdminModule.Resources.Methods
{
    public class RemoveCommand
    {
        public static bool Remove<T>(T _object, int _id, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = $"DELETE{_object?.GetType().Name.ToUpper()}",
                    IdToDelete = _id
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

                    if (response.Message == "DELETED")
                    {
                        if (_object is Aircraft)
                            WorkingObjectsRepository.Aircrafts?.Remove
                                (WorkingObjectsRepository.Aircrafts.Where(e => e.Id == _id)
                                .SingleOrDefault()
                                ?? throw new Exception("missing"));
                        else if (_object is Airport)
                            WorkingObjectsRepository.Airports?.Remove
                                (WorkingObjectsRepository.Airports.Where(e => e.Id == _id)
                                .SingleOrDefault()
                                ?? throw new Exception("missing"));
                        else if (_object is Terminal)
                            WorkingObjectsRepository.Terminals?.Remove
                                (WorkingObjectsRepository.Terminals.Where(e => e.Id == _id)
                                .SingleOrDefault()
                                ?? throw new Exception("missing"));
                        else if (_object is Gate)
                            WorkingObjectsRepository.Gates?.Remove
                                (WorkingObjectsRepository.Gates.Where(e => e.Id == _id)
                                .SingleOrDefault()
                                ?? throw new Exception("missing"));
                        else if (_object is Country)
                            WorkingObjectsRepository.Countries?.Remove
                                (WorkingObjectsRepository.Countries.Where(e => e.Id == _id)
                                .SingleOrDefault()
                                ?? throw new Exception("missing"));
                        else if (_object is City)
                            WorkingObjectsRepository.Cities?.Remove
                                (WorkingObjectsRepository.Cities.Where(e => e.Id == _id)
                                .SingleOrDefault()
                                ?? throw new Exception("missing"));
                        else if (_object is Flight)
                            WorkingObjectsRepository.Flights?.Remove
                                (WorkingObjectsRepository.Flights.Where(e => e.Id == _id)
                                .SingleOrDefault()
                                ?? throw new Exception("missing"));
                        else if (_object is Seat)
                            WorkingObjectsRepository.Seats?.Remove
                                (WorkingObjectsRepository.Seats.Where(e => e.Id == _id)
                                .SingleOrDefault()
                                ?? throw new Exception("missing"));
                        else if (_object is SeatType)
                            WorkingObjectsRepository.SeatTypes?.Remove
                                (WorkingObjectsRepository.SeatTypes.Where(e => e.Id == _id)
                                .SingleOrDefault()
                                ?? throw new Exception("missing"));
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
