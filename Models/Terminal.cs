using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable]
    public class Terminal
    {
        public int Id { get; set; }
        [MaxLength(1)]
        public string Name { get; set; } = null!;
        public virtual List<Gate> Gates { get; set; } = [];
        public int AirportId { get; set; }
        public virtual Airport Airport { get; set; } = null!;
    }
}
