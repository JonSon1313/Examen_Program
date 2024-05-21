using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable]
    public class City
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public int CountryId { get; set; }
        public virtual Country Country { get; set; } = null!;   
    }
}
