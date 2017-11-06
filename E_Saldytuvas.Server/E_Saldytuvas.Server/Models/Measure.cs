using System.ComponentModel.DataAnnotations;

namespace E_Saldytuvas.Server.Models
{
    public class Measure
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
