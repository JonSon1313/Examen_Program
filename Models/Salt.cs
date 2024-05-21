using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable]
    public class Salt
    {
        public int Id { get; set; }
        public string SaltString { get; set; } = null!;
        [MaxLength(100)]
        public string Login { get; set; } = null!;
    }
}
