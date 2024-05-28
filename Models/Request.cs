using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class Request
    {
        public string Message { get; set; } = null!;

        public int IdToDelete { get; set; }
        public int ObjectId { get; set; }

        public Country? Country { get; set; }
        public City? City { get; set; }

        public Airport? Airport { get; set; }
        public Terminal? Terminal { get; set; }
        public Gate? Gate { get; set; }

        public Aircraft? Aircraft { get; set; }
        public Seat? Seat { get; set; }
        public SeatType? SeatType { get; set; }

        public Flight? Flight { get; set; }

        public Administrator? Administrator { get; set; }
        public Client? Client { get; set; }

        public List<Ticket>? Ticket { get; set; }

        public DateTime? Date {  get; set; }
    }
}
