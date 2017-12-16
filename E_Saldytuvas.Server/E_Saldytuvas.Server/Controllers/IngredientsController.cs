using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using E_Saldytuvas.Server.Data;
using E_Saldytuvas.Server.Models;
using E_Saldytuvas.Server.Services;

namespace E_Saldytuvas.Server.Controllers
{
    [Route("api/[controller]")]
    public class IngredientsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly IIngredientService _ingredientService;

        public IngredientsController(ApplicationDbContext dbContext, IIngredientService ingredientService)
        {
            _dbContext = dbContext;
            _ingredientService = ingredientService;

            if (!_dbContext.Ingredients.Any())
            {
                _dbContext.Ingredients
                    .Add(new Ingredient { Name = "Sūris", Amount = 200, Measure = _dbContext.Measures.FirstOrDefault(m => m.Id == 1), UserId = 1});

                _dbContext.SaveChanges();
            }
        }

        // GET api/ingredients
        [HttpGet]
        public IEnumerable<Ingredient> GetIngredients()
        {
            return _ingredientService.GetIngredients();
        }

        // GET api/ingredients/5
        [HttpGet("{ingredientId}", Name = "GetIngredient")]
        public IActionResult GetIngredient(int ingredientId)
        {
            var ingredient = _ingredientService.GetIngredient(ingredientId);

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
            if (_ingredientService.AddIngredient(ingredient))
            {
                return CreatedAtRoute("GetIngredient", new { ingredientId = ingredient.Id }, ingredient);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/ingredients/5
        [HttpPut("{ingredientId}")]
        public IActionResult UpdateIngredient(long ingredientId, [FromBody] Ingredient ingr)
        {
            var result = _ingredientService.UpdateIngredient(ingredientId, ingr);

            if (result == -1)
            {
                return BadRequest();
            }

            if (result == -2)
            {
                return NotFound();
            }

            return new NoContentResult();
        }

        // DELETE api/ingredients/5
        [HttpDelete("{ingredientId}")]
        public IActionResult DeleteIngredient(int ingredientId)
        {
            if (_ingredientService.DeleteIngredient(ingredientId) == false)
            {
                return NotFound();
            }

            return new NoContentResult();
        }

        [HttpGet("user/{userId}")]
        public IEnumerable<Ingredient> GetUserIngredients(long userId)
        {
            try
            {
                return _ingredientService.GetUserIngredients(userId);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
