using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;

        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = null!;
    }
}
