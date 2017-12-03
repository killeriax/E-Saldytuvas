using E_Saldytuvas.Server.Models;
using System.Collections.Generic;

namespace E_Saldytuvas.Server.Services
{
    public interface IRecipeService
    {
        IEnumerable<Recipe> GetRecipes();
        IEnumerable<Recipe> GetUserRecipes(long userId);
        Recipe GetRecipe(int recipeId);
        bool AddRecipe(Recipe recipe);
        int UpdateRecipe(long recipeId, Recipe rcp);
        bool DeleteRecipe(int recipeId);
    }
}
