using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using E_Saldytuvas.Server.Data;
using E_Saldytuvas.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Saldytuvas.Server.Controllers
{
    [Route("api/[controller]")]
    public class CookedMealsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CookedMealsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            if (_dbContext.CookedMeals.Count() == 0)
            {
                _dbContext.CookedMeals
                    .Add(new CookedMeal { Size = 0.5 });

                _dbContext.SaveChanges();
            }
        }

        // GET api/cookedMeals
        [HttpGet]
        public IEnumerable<CookedMeal> GetCookedMeals()
        {
            var cookedMeals = _dbContext.CookedMeals
                .Include(cm => cm.Recipe)
                .ToList();
            return cookedMeals;
        }

        // GET api/cookedMeals/5
        [HttpGet("{cookedMealId}", Name = "GetCookedMeal")]
        public IActionResult GetCookedMeal(int cookedMealId)
        {
            var cookedMeal = _dbContext.CookedMeals
                .Include(cm => cm.Recipe)
                .FirstOrDefault(cm => cm.Id == cookedMealId);

            if (cookedMeal == null)
            {
                return NotFound();
            }
            return new ObjectResult(cookedMeal);
        }

        // POST api/cookedMeals
        [HttpPost]
        public IActionResult AddCookedMeal([FromBody] CookedMeal cookedMeal)
        {
            if (cookedMeal == null)
            {
                return BadRequest();
            }

            _dbContext.CookedMeals
                .Add(cookedMeal);

            _dbContext.SaveChanges();

            return CreatedAtRoute("GetCookedMeal", new { cookedMealId = cookedMeal.Id }, cookedMeal);
        }

        // PUT api/cookedMeals/5
        [HttpPut("{cookedMealId}")]
        public IActionResult UpdateCookedMeal(long cookedMealId, [FromBody] CookedMeal cmeal)
        {
            if (cmeal == null || cmeal.Id != cookedMealId)
            {
                return BadRequest();
            }

            var cookedMeal = _dbContext.CookedMeals
                .Include(cm => cm.Recipe)
                .FirstOrDefault(cm => cm.Id == cookedMealId);

            if (cookedMeal == null)
            {
                return NotFound();
            }

            cookedMeal.Size = cmeal.Size;

            _dbContext.CookedMeals
                .Update(cookedMeal);

            _dbContext.SaveChanges();

            return new NoContentResult();
        }

        // DELETE api/cookedMeals/5
        [HttpDelete("{cookedMealId}")]
        public IActionResult DeleteCookedMeal(int cookedMealId)
        {
            var cookedMeal = _dbContext.CookedMeals
                .Include(cm => cm.Recipe)
                .FirstOrDefault(cm => cm.Id == cookedMealId);

            if (cookedMeal == null)
            {
                return NotFound();
            }

            _dbContext.CookedMeals
                .Remove(cookedMeal);

            _dbContext.SaveChanges();

            return new NoContentResult();
        }
    }
}
