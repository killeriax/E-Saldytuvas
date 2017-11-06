using System.ComponentModel.DataAnnotations;

namespace E_Saldytuvas.Server.Models
{
    public class CookedMeal
    {
        public int Id { get; set; }

        public Recipe Recipe { get; set; }

        [Required]
        public double Size { get; set; }
    }
}
