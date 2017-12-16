using E_Saldytuvas.Server.Models;
using System.Collections.Generic;

namespace E_Saldytuvas.Server.Services
{
    public interface IIngredientService
    {
        IEnumerable<Ingredient> GetIngredients();
        Ingredient GetIngredient(int ingredientId);
        bool AddIngredient(Ingredient ingredient);
        int UpdateIngredient(long ingredientId, Ingredient ingr);
        bool DeleteIngredient(int ingredientId);
        IEnumerable<Ingredient> GetUserIngredients(long userId);
    }
}
