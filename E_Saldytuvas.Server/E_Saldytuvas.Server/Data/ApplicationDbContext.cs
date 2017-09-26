using Microsoft.EntityFrameworkCore;
using E_Saldytuvas.Server.Models;

namespace E_Saldytuvas.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CookedMeal> CookedMeals { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Measure> Measures { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
