using E_Saldytuvas.Server.Models;
using System.Collections.Generic;

namespace E_Saldytuvas.Server.Services
{
    public interface ICookedMealService
    {
        IEnumerable<CookedMeal> GetCookedMeals();
        CookedMeal GetCookedMeal(int cookedMealId);
        bool AddCookedMeal(CookedMeal cookedMeal);
        int UpdateCookedMeal(long cookedMealId, CookedMeal cmeal);
        bool DeleteCookedMeal(int cookedMealId);
    }
}
