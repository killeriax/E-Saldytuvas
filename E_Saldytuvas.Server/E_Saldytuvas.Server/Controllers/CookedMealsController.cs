using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using E_Saldytuvas.Server.Data;
using E_Saldytuvas.Server.Models;
using E_Saldytuvas.Server.Services;

namespace E_Saldytuvas.Server.Controllers
{
    [Route("api/[controller]")]
    public class CookedMealsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly ICookedMealService _cookedMealService;

        public CookedMealsController(ApplicationDbContext dbContext, ICookedMealService cookedMealService)
        {
            _dbContext = dbContext;
            _cookedMealService = cookedMealService;

            if (!_dbContext.CookedMeals.Any())
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
            return _cookedMealService.GetCookedMeals();
        }

        // GET api/cookedMeals/5
        [HttpGet("{cookedMealId}", Name = "GetCookedMeal")]
        public IActionResult GetCookedMeal(int cookedMealId)
        {
            var cookedMeal = _cookedMealService.GetCookedMeal(cookedMealId);

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
            if (_cookedMealService.AddCookedMeal(cookedMeal))
            {
                return CreatedAtRoute("GetCookedMeal", new { cookedMealId = cookedMeal.Id }, cookedMeal);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/cookedMeals/5
        [HttpPut("{cookedMealId}")]
        public IActionResult UpdateCookedMeal(long cookedMealId, [FromBody] CookedMeal cmeal)
        {
            var result = _cookedMealService.UpdateCookedMeal(cookedMealId, cmeal);

            if ( result == -1)
            {
                return BadRequest();
            }

            if ( result == -2)
            {
                return NotFound();
            }

            return new NoContentResult();
        }

        // DELETE api/cookedMeals/5
        [HttpDelete("{cookedMealId}")]
        public IActionResult DeleteCookedMeal(int cookedMealId)
        {
            if (_cookedMealService.DeleteCookedMeal(cookedMealId) == false)
            {
                return NotFound();
            }

            return new NoContentResult();
        }
    }
}
