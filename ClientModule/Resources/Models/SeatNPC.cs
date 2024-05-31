using ClientModule.Resources.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace ClientModule.Resources.Models
{
    public partial class SeatNPC : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string? name;
        [ObservableProperty]
        private bool reserved;
        [ObservableProperty]
        private int seatTypeId;
        [ObservableProperty]
        private string? seatType;
        [ObservableProperty]
        private int aircraftId;
        [ObservableProperty]
        private int flightId;

        public static SeatNPC ConvertFromSeatToNew(Seat seat)
        {
            var seatTypeName = WorkingObjectsRepository.SeatTypes?.Where(s=>s.Id == seat.SeatTypeId).Select(s=>s.Name).FirstOrDefault();

            return new()
            {
                Id = seat.Id,
                Name = seat.Name,
                Reserved = seat.Reserved,
                SeatTypeId = seat.SeatTypeId,
                SeatType = seatTypeName,
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