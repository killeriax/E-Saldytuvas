using System.Collections.Generic;

namespace E_Saldytuvas.Server.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public string Description { get; set; }
    }
}
