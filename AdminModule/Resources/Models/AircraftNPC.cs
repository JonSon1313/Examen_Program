using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace AdminModule.Resources.Models
{
    public partial class AircraftNPC : ObservableObject
    {
        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public string manufacturer;
        [ObservableProperty]
        public string model;
        [ObservableProperty]
        public string tailNumber;

        public static AircraftNPC ConvertFromAircraftToNew(Aircraft aircraft)
        {
            return new()
            {
                Id = aircraft.Id,
                Manufacturer = aircraft.Manufacturer,
                Model = aircraft.Model,
                TailNumber = aircraft.TailNumber
            }; 
        }
        public Aircraft ConvertToAircraft()
        {
            return new()
            {
                Id = this.Id,
                Manufacturer = this.Manufacturer,
                Model = this.Model,
                TailNumber = this.TailNumber
            };
        }
        public override string ToString()
        {
            return $"{Manufacturer} {Model} : {TailNumber}";
        }
    }
}
