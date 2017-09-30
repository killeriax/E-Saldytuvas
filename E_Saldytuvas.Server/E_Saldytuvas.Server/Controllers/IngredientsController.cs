using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_Saldytuvas.Server.Data;
using E_Saldytuvas.Server.Models;

namespace E_Saldytuvas.Server.Controllers
{
    [Route("api/[controller]")]
    public class IngredientsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public IngredientsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            if (_dbContext.Ingredients.Count() == 0)
            {
                _dbContext.Ingredients.Add(new Ingredient { Name = "Sūris", Amount = 200 });
                _dbContext.SaveChanges();
            }
        }

        // GET api/ingredients
        [HttpGet]
        public IEnumerable<Ingredient> GetIngredients()
        {
            var ingredients = _dbContext.Ingredients
                .ToList();
            return ingredients;
        }

        // GET api/ingredients/5
        [HttpGet("{ingredientId}", Name = "GetIngredient")]
        public IActionResult GetIngredient(int ingredientId)
        {
            var ingredient = _dbContext.Ingredients.FirstOrDefault(i => i.Id == ingredientId);
            if (ingredient == null)
            {
                return NotFound();
            }
            return new ObjectResult(ingredient);
        }

        // POST api/ingredients
        [HttpPost]
        public IActionResult AddIngredient([FromBody] Ingredient ingredient)
        {
            if (ingredient == null)
            {
                return BadRequest();
            }

            _dbContext.Ingredients.Add(ingredient);
            _dbContext.SaveChanges();

            return CreatedAtRoute("GetIngredient", new { ingredientId = ingredient.Id }, ingredient);
        }

        // PUT api/ingredients/5
        [HttpPut("{ingredientId}")]
        public IActionResult UpdateIngredient(long ingredientId, [FromBody] Ingredient ingr)
        {
            if (ingr == null || ingr.Id != ingredientId)
            {
                return BadRequest();
            }

            var ingredient = _dbContext.Ingredients.FirstOrDefault(i => i.Id == ingredientId);
            if (ingredient == null)
            {
                return NotFound();
            }

            ingredient.Name = ingr.Name;
            ingredient.Amount = ingr.Amount;

            _dbContext.Ingredients.Update(ingredient);
            _dbContext.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/ingredients/5
        [HttpDelete("{ingredientId}")]
        public IActionResult DeleteIngredient(int ingredientId)
        {
            var ingredient = _dbContext.Ingredients.FirstOrDefault(i => i.Id == ingredientId);
            if (ingredient == null)
            {
                return NotFound();
            }

            _dbContext.Ingredients.Remove(ingredient);
            _dbContext.SaveChanges();
            return new NoContentResult();
        }
    }
}
