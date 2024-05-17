using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
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
