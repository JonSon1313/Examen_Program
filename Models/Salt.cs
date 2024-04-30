using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Salt
    {
        public int Id { get; set; }
        public string SaltString { get; set; } = null!;
        [MaxLength(100)]
        public string Login { get; set; } = null!;
    }
}
