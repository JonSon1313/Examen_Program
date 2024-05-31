using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace ClientModule.Resources.Models
{
    public partial class CityNPC : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string? name;
        [ObservableProperty]
        private int countryId;

        public static CityNPC ConvertFromCityToNew(City city)
        {
            return new()
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.CountryId,
            };
        }
        public City ConvertToCity()
        {
            return new()
            {
                Id = this.Id,
                Name = this.Name ?? "",
                CountryId = this.CountryId,
            };
        }

        public override string ToString()
        {
            return Name;
        }
    }
}