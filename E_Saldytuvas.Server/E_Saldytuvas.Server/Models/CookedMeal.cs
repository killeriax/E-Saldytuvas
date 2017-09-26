using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Saldytuvas.Server.Models
{
    public class CookedMeal
    {
        public int Id { get; set; }

        public Recipe Recipe { get; set; }

        public double Size { get; set; }
    }
}
