using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using ClientModule.Resources.Repositories;

namespace ClientModule.Resources.Models
{
    public partial class FlightNPC : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string number;
        [ObservableProperty]
        private int aircraftId;
        [ObservableProperty]
        private DateTime departureTime;
        [ObservableProperty]
        private int fromId;
        [ObservableProperty]
        private string departureCity;
        [ObservableProperty]
        private string departureCountry;
        [ObservableProperty]
        private string arrivaleCity;
        [ObservableProperty]
        private string arrivaleCountry;
        [ObservableProperty]
        private int toId;
        [ObservableProperty]
        private decimal basePrice;


        public static FlightNPC ConvertFromFlightToNew(Flight flight)
        {
            var depAirport = WorkingObjectsRepository.Airports?.Where(c => c.Id == WorkingObjectsRepository.Gates?
            .Where(c => c.Id == flight.FromId).Select(c => c.AirportId).SingleOrDefault()).SingleOrDefault();
            var arrAirport = WorkingObjectsRepository.Airports?.Where(c => c.Id == WorkingObjectsRepository.Gates?
            .Where(c => c.Id == flight.ToId).Select(c => c.AirportId).SingleOrDefault()).SingleOrDefault();

            var depCity = WorkingObjectsRepository.Cities?.Where(c => c.Id == depAirport?.CityId).Select(c => c.Name).SingleOrDefault();
            var depCountry = WorkingObjectsRepository.Countries?.Where(c => c.Id == depAirport!.CountryId).Select(c => c.Name).SingleOrDefault();
            var arrCity = WorkingObjectsRepository.Cities?.Where(c => c.Id == arrAirport?.CityId).Select(c => c.Name).SingleOrDefault();
            var arrCountry = WorkingObjectsRepository.Countries?.Where(c => c.Id == arrAirport!.CountryId).Select(c => c.Name).SingleOrDefault();

            return new()
            {
                Id = flight.Id,
                Number = flight.Number,
                AircraftId = flight.AircraftId,
                DepartureTime = flight.DepartureTime,
                FromId = flight.FromId,
                DepartureCity = depCity ?? "",
                DepartureCountry = depCountry ?? "",
                ToId = flight.ToId,
                ArrivaleCity = arrCity ?? "",
                ArrivaleCountry = arrCountry ?? "",
                BasePrice = flight.BasePrice
            };
        }
        public Flight ConvertToFlight()
        {
            return new()
            {
                Id = this.Id,
                Number = this.Number,
                AircraftId = this.AircraftId,
                DepartureTime = this.DepartureTime,
                FromId = this.FromId,
                ToId = this.ToId,
                BasePrice = this.BasePrice
            };
        }
    }
}