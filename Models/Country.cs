using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Country
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}
