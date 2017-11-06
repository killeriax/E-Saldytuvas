using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Saldytuvas.Server.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Title { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
