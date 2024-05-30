using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace ClientModule.Resources.Models
{
    public partial class AirportNPC : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string fullName;
        [ObservableProperty]
        private string iATACode;
        [ObservableProperty]
        private string iCAOCode;

        [ObservableProperty]
        private int cityId;
        [ObservableProperty]
        private int countryId;

        public static AirportNPC ConvertFromAirportToNew(Airport airport)
        {

            return new()
            {
                Id = airport.Id,
                FullName = airport.FullName,
                IATACode = airport.IATACode,
                ICAOCode = airport.ICAOCode,
                CityId = airport.CityId,
                CountryId = airport.CountryId
            };
        }

        public Airport ConvertToAirport()
        {
            return new()
            {
                Id = this.Id,
                FullName = this.FullName,
                IATACode = this.IATACode,
                ICAOCode = this.ICAOCode,
                CityId = this.CityId,
                CountryId = this.CountryId
            };
        }

        public override string ToString()
        {
            return $"{FullName} IATA:{IATACode} ICAO:{ICAOCode}";
        }
    }
}
