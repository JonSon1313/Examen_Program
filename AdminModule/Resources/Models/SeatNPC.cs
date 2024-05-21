using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminModule.Resources.Models
{
    public partial class SeatNPC : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private bool reserved;
        [ObservableProperty]
        private int seatTypeId;
        [ObservableProperty]
        private int aircraftId;
        [ObservableProperty]
        private int flightId;

        public static SeatNPC ConvertFromSeatToNew(Seat seat)
        {

            return new()
            {
                Id = seat.Id,
                Name = seat.Name,
                Reserved = seat.Reserved,
                SeatTypeId = seat.SeatTypeId,
                AircraftId = seat.AircraftId,
                FlightId = seat.FlightId,
            };
        }

        public Seat ConvertToSeat()
        {
            return new()
            {
                Id = this.Id,
                Name = this.Name ?? "",
                Reserved = this.Reserved,
                SeatTypeId = this.SeatTypeId,
                AircraftId = this.AircraftId,
                FlightId = this.FlightId,
            };
        }
    }
}
