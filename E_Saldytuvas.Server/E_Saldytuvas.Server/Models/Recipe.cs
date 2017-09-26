using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Saldytuvas.Server.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<Ingredient> Igredients { get; set; }

        public string Description { get; set; }
    }
}
