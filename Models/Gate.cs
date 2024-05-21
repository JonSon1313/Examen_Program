using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable]
    public class Gate
    {
        public int Id { get; set; }
        [MaxLength(3)]
        public string Name { get; set; } = null!;

        public int TerminalId { get; set; }
        public virtual Terminal Terminal { get; set; } = null!;
        public int AirportId { get; set; }
        public virtual Airport Airport { get; set; } = null!;

        public virtual List<Flight> In { get; set; } = [];
        public virtual List<Flight> Out { get; set; } = [];
    }
}
