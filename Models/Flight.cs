﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;

        public Aircraft Aircraft { get; set; } = null!;

        public DateTime DepartureTime { get; set; }
        public Gate From { get; set; } = null!;
        public Gate To { get; set; } = null!;

        public virtual List<Seat> Seats { get; set; } = [];
    }
}
