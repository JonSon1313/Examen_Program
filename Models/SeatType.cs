using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable]
    public class SeatType
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; } = null!;
    }
}
