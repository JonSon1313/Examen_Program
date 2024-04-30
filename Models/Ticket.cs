using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Number { get; set; } = null!;

        public DateTime SaleTime { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; } = null!;

        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; } = null!;

        public double Discount { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
