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
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public RecipesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            if (_dbContext.Recipes.Count() == 0)
            {
                _dbContext.Recipes.Add(new Recipe { Title = "Sumuštinis", Description = "Paimame vieną riekę, užtepame sviestu, uždedame dešros, pomidorų ir patiekiame." });
                _dbContext.SaveChanges();
            }
        }

        // GET api/recipes
        [HttpGet]
        public IEnumerable<Recipe> GetRecipes()
        {
            var recipes = _dbContext.Recipes
                .ToList();
            return recipes;
        }

        // GET api/recipes/5
        [HttpGet("{recipeId}", Name = "GetRecipe")]
        public IActionResult GetRecipe(int recipeId)
        {
            var recipe = _dbContext.Recipes.FirstOrDefault(r => r.Id == recipeId);
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
            if (recipe == null)
            {
                return BadRequest();
            }

            _dbContext.Recipes.Add(recipe);
            _dbContext.SaveChanges();

            return CreatedAtRoute("GetRecipe", new { recipeId = recipe.Id }, recipe);
        }

        // PUT api/recipes/5
        [HttpPut("{recipeId}")]
        public IActionResult UpdateRecipe(long recipeId, [FromBody] Recipe rcp)
        {
            if (rcp == null || rcp.Id != recipeId)
            {
                return BadRequest();
            }

            var recipe = _dbContext.Recipes.FirstOrDefault(r => r.Id == recipeId);
            if (recipe == null)
            {
                return NotFound();
            }

            recipe.Title = rcp.Title;
            recipe.Description = rcp.Description;

            _dbContext.Recipes.Update(recipe);
            _dbContext.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/recipes/5
        [HttpDelete("{recipeId}")]
        public IActionResult DeleteRecipe(int recipeId)
        {
            var recipe = _dbContext.Recipes.FirstOrDefault(r => r.Id == recipeId);
            if (recipe == null)
            {
                return NotFound();
            }

            _dbContext.Recipes.Remove(recipe);
            _dbContext.SaveChanges();
            return new NoContentResult();
        }
    }
}
