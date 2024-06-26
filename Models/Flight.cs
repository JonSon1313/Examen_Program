﻿using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable]
    public class Flight
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string Number { get; set; } = null!;

        public int AircraftId {  get; set; }
        public Aircraft Aircraft { get; set; } = null!;

        public DateTime DepartureTime { get; set; }

        public int FromId { get; set; }
        public Gate From { get; set; } = null!;

        public int ToId { get; set; }
        public Gate To { get; set; } = null!;

        public virtual List<Seat> Seats { get; set; } = [];
        
        public decimal BasePrice { get; set; }
     }
}
