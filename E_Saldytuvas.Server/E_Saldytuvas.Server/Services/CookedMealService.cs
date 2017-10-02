using E_Saldytuvas.Server.Data;
using E_Saldytuvas.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Saldytuvas.Server.Services
{
    public class CookedMealService : ICookedMealService
    {
        private readonly ApplicationDbContext _dbContext;

        public CookedMealService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<CookedMeal> GetCookedMeals()
        {
            var cookedMeals = _dbContext.CookedMeals
                .Include(cm => cm.Recipe)
                .ToList();

            return cookedMeals;
        }

        public CookedMeal GetCookedMeal(int cookedMealId)
        {
            var cookedMeal = _dbContext.CookedMeals
                .Include(cm => cm.Recipe)
                .FirstOrDefault(cm => cm.Id == cookedMealId);

            return cookedMeal;
        }

        public bool AddCookedMeal(CookedMeal cookedMeal)
        {
            if (cookedMeal == null)
            {
                return false;
            }

            _dbContext.CookedMeals
                .Add(cookedMeal);

            _dbContext.SaveChanges();

            return true;
        }

        public int UpdateCookedMeal(long cookedMealId, CookedMeal cmeal)
        {
            if (cmeal == null || cmeal.Id != cookedMealId)
            {
                return -1;
            }

            var cookedMeal = _dbContext.CookedMeals
                .Include(cm => cm.Recipe)
                .FirstOrDefault(cm => cm.Id == cookedMealId);

            if (cookedMeal == null)
            {
                return -2;
            }

            cookedMeal.Size = cmeal.Size;

            _dbContext.CookedMeals
                .Update(cookedMeal);

            _dbContext.SaveChanges();

            return 1;
        }

        public bool DeleteCookedMeal(int cookedMealId)
        {
            var cookedMeal = _dbContext.CookedMeals
                .Include(cm => cm.Recipe)
                .FirstOrDefault(cm => cm.Id == cookedMealId);

            if (cookedMeal == null)
            {
                return false;
            }

            _dbContext.CookedMeals
                .Remove(cookedMeal);

            _dbContext.SaveChanges();

            return true;
        }
    }
}
