using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace ClientModule.Resources.Models
{
    public partial class CountryNPC : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string? name;
        public static CountryNPC ConvertFromCountryToNew(Country country)
        {

            return new()
            {
                Id = country.Id,
                Name = country.Name,
            };
        }

        public Country ConvertToCountry()
        {
            return new()
            {
                Id = this.Id,
                Name = this.Name ?? ""
            };
        }

        public override string ToString()
        {
            return Name ?? "";
        }
    }
}
