using E_Saldytuvas.Server.Data;
using E_Saldytuvas.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Saldytuvas.Server.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly ApplicationDbContext _dbContext;

        public IngredientService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Ingredient> GetIngredients()
        {
            var ingredients = _dbContext.Ingredients
                .Include(i => i.Measure)
                .ToList();

            return ingredients;
        }

        public Ingredient GetIngredient(int ingredientId)
        {
            var ingredient = _dbContext.Ingredients
                .Include(i => i.Measure)
                .FirstOrDefault(i => i.Id == ingredientId);

            return ingredient;
        }

        public bool AddIngredient(Ingredient ingredient)
        {
            if (ingredient == null)
            {
                return false;
            }

            _dbContext.Ingredients
                .Add(ingredient);

            _dbContext.SaveChanges();

            return true;
        }

        public int UpdateIngredient(long ingredientId, Ingredient ingr)
        {
            if (ingr == null || ingr.Id != ingredientId)
            {
                return -1;
            }

            var ingredient = _dbContext.Ingredients
                .FirstOrDefault(i => i.Id == ingredientId);

            if (ingredient == null)
            {
                return -2;
            }

            ingredient.Name = ingr.Name;
            ingredient.Amount = ingr.Amount;

            _dbContext.Ingredients
                .Update(ingredient);

            _dbContext.SaveChanges();

            return 1;
        }

        public bool DeleteIngredient(int ingredientId)
        {
            var ingredient = _dbContext.Ingredients
                .FirstOrDefault(i => i.Id == ingredientId);

            if (ingredient == null)
            {
                return false;
            }

            _dbContext.Ingredients
                .Remove(ingredient);

            _dbContext.SaveChanges();

            return true;
        }
    }
}
