using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable]
    public class Country
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public virtual List<City> Cities { get; set; } = [];
        public virtual List<Airport> Airports { get; set; } = [];
    }
}
