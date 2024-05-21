using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable]
    public class Aircraft
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Manufacturer { get; set; } = null!;
        [MaxLength(100)]
        public string Model { get; set; } = null!;
        [MaxLength(10)]
        public string TailNumber { get; set; } = null!;

        public virtual List<Flight> Flights { get; set;} = [];
    }
}
