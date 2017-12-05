using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace E_Saldytuvas.Server.Models
{
    public class User
    {
        [Required]
        public string AuthId { get; set; }

        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        public List<Ingredient> Ingredients { get; set; }
        [JsonIgnore]
        public List<Recipe> Recipes { get; set; }

        public List<CookedMeal> CookedMeals { get; set; }
    }
}
