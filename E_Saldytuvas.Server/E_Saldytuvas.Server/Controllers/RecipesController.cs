using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using E_Saldytuvas.Server.Models;
using E_Saldytuvas.Server.Services;

namespace E_Saldytuvas.Server.Controllers
{
    [Route("api/[controller]")]
    public class RecipesController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // GET api/recipes
        [HttpGet]
        public IEnumerable<Recipe> GetRecipes()
        {
            return _recipeService.GetRecipes();
        }

        [HttpGet("user/{userId}")]
        public IEnumerable<Recipe> GetUserRecipes(long userId)
        {
            try
            {
                return _recipeService.GetUserRecipes(userId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        // GET api/recipes/5
        [HttpGet("{recipeId}", Name = "GetRecipe")]
        public IActionResult GetRecipe(int recipeId)
        {
            var recipe = _recipeService.GetRecipe(recipeId);

            if (recipe == null)
            {
                return NotFound();
            }
            return new ObjectResult(recipe);
        }

        // POST api/recipes
        [HttpPost]
        public IActionResult AddRecipe([FromBody] Recipe recipe)
        {
            if (_recipeService.AddRecipe(recipe))
            {
                return CreatedAtRoute("GetRecipe", new { recipeId = recipe.Id }, recipe);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/recipes/5
        [HttpPut("{recipeId}")]
        public IActionResult UpdateRecipe(long recipeId, [FromBody] Recipe rcp)
        {
            var result = _recipeService.UpdateRecipe(recipeId, rcp);

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

        // DELETE api/recipes/5
        [HttpDelete("{recipeId}")]
        public IActionResult DeleteRecipe(int recipeId)
        {
            if (_recipeService.DeleteRecipe(recipeId) == false)
            {
                return NotFound();
            }

            return new NoContentResult();
        }
    }
}
