using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Aircraft
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Manufacturer { get; set; } = null!;
        [MaxLength(100)]
        public string Model { get; set; } = null!;
        [MaxLength(10)]
        public string TailNumber { get; set; } = null!;
    }
}
