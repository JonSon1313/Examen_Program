using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class City
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public int CountryId { get; set; }
        public virtual Country Country { get; set; } = null!;   
    }
}
