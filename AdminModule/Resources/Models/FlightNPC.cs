using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminModule.Resources.Models
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
            return new()
            {
                Id = flight.Id,
                Number = flight.Number,
                AircraftId = flight.AircraftId,
                DepartureTime = flight.DepartureTime,
                FromId = flight.FromId,
                ToId = flight.ToId,

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
