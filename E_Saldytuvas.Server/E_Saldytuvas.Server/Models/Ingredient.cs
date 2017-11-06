using System.ComponentModel.DataAnnotations;

namespace E_Saldytuvas.Server.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public double Amount { get; set; }

        public Measure Measure { get; set; }
    }
}
