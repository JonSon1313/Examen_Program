using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Response
    {
        public string Message { get; set; } = null!;

        public List<Airport>? Airports { get; set; }

        public List<Flight>? Flight { get; set; }

        public int? TicketId { get; set; }
        public int? TicketNumber { get; set; }

        public Administrator? Administrator { get; set; }
        public Client? Client { get; set; }
    }
}
