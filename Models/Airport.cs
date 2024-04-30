﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Airport
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string FullName { get; set; } = null!;
        [MaxLength(3)]
        public string IATACode { get; set; } = null!;
        [MaxLength(4)]
        public string ICAOCode { get; set; } = null!;

        public int CityId { get; set; }
        public virtual City City { get; set; } = null!;
        public int CountryId { get; set; }
        public virtual Country Country { get; set; } = null!;

        public virtual List<Terminal> Terminals { get; set; } = [];
        public virtual List<Gate> Gates { get; set; } = []; 
    }
}
