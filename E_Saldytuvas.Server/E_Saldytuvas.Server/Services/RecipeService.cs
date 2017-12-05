using E_Saldytuvas.Server.Data;
using E_Saldytuvas.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Saldytuvas.Server.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext _dbContext;

        public RecipeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Recipe> GetRecipes()
        {
            var recipes = _dbContext.Recipes
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .ToList();

            return recipes;
        }

        public IEnumerable<Recipe> GetUserRecipes(long userId)
        {
            /*var user = _dbContext.Users
                .Include(u => u.Recipes)
                .SingleOrDefault(u => u.Id == userId);

            if(user == null)
                throw new Exception("User not found");*/

            var recipes = _dbContext.Recipes
                .Include(r => r.Ingredients)
                .Where(r => r.User.Id == userId)
                .ToList();

            return recipes;
        }

        public Recipe GetRecipe(int recipeId)
        {
            var recipe = _dbContext.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefault(r => r.Id == recipeId);

            return recipe;
        }

        public bool AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                return false;
            }


            _dbContext.Recipes
                .Add(recipe);

            _dbContext.SaveChanges();

            return true;
        }

        public int UpdateRecipe(long recipeId, Recipe rcp)
        {
            if (rcp == null || rcp.Id != recipeId)
            {
                return -1;
            }

            var recipe = _dbContext.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefault(r => r.Id == recipeId);

            if (recipe == null)
            {
                return -2;
            }

            recipe.Title = rcp.Title;
            recipe.Description = rcp.Description;

            _dbContext.Recipes
                .Update(recipe);

            _dbContext.SaveChanges();

            return 1;
        }

        public bool DeleteRecipe(int recipeId)
        {
            var recipe = _dbContext.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefault(r => r.Id == recipeId);

            if (recipe == null)
            {
                return false;
            }

            _dbContext.Recipes
                .Remove(recipe);

            _dbContext.SaveChanges();

            return true;
        }
    }
}
