﻿using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable]
    public class Administrator
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [MaxLength(50)]
        public string MiddleName { get; set; } = null!;
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [MaxLength(100)]
        public string Login {  get; set; } = null!;
        [MaxLength(100)]
        public string Password { get; set; } = null!;

        [MaxLength(15)]
        public string PhoneNumber { get; set; } = null!;
        [MaxLength(100)]
        public string Email { get; set; } = null!;
    }
}
