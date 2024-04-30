﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Gate
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int TerminalId { get; set; }
        public virtual Terminal Terminal { get; set; } = null!;
        public int AirportId { get; set; }
        public virtual Airport Airport { get; set; } = null!;
    }
}