using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable]
    public class Seat
    {
        public int Id { get; set; }
        [MaxLength(3)]
        public string Name { get; set; } = null!;

        public bool Reserved { get; set; }

        public int SeatTypeId { get; set; }
        public virtual SeatType SeatType { get; set; } = null!;

        public int AircraftId { get; set; }
        public virtual Aircraft Aircraft { get; set; } = null!;

        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; } = null!;
    }
}
