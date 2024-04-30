using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string Number { get; set; } = null!;

        public int ClientId { get; set; }
        public virtual Client Client { get; set; } = null!;

        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; } = null!;
    }
}
