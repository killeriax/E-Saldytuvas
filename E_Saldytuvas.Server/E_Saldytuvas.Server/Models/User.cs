using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Saldytuvas.Server.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public List<Recipe> Recipes { get; set; }

        public List<CookedMeal> CookedMeals { get; set; }
    }
}
