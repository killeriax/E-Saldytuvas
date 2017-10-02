using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using E_Saldytuvas.Server.Data;
using E_Saldytuvas.Server.Models;

namespace E_Saldytuvas.Server.Controllers
{
    [Route("api/[controller]")]
    public class MeasuresController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public MeasuresController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            if (_dbContext.Measures.Count() == 0)
            {
                _dbContext.Measures
                    .Add(new Measure { Name = "Kg" });

                _dbContext.SaveChanges();
            }
        }

        // GET: api/measures
        [HttpGet]
        public IEnumerable<Measure> GetMeasures()
        {
            var measures = _dbContext.Measures
                .ToList();

            return measures;
        }

        // GET api/measures/5
        [HttpGet("{measureId}", Name = "GetMeasure")]
        public IActionResult GetMeasure(int measureId)
        {
            var measure = _dbContext.Measures
                .FirstOrDefault(m => m.Id == measureId);

            if (measure == null)
            {
                return NotFound();
            }

            return new ObjectResult(measure);
        }

        // POST api/measures
        [HttpPost]
        public IActionResult AddMeasure([FromBody] Measure measure)
        {
            if (measure == null)
            {
                return BadRequest();
            }

            _dbContext.Measures
                .Add(measure);

            _dbContext.SaveChanges();

            return CreatedAtRoute("GetMeasure", new { measureId = measure.Id }, measure);
        }

        // PUT api/measures/5
        [HttpPut("{measureId}")]
        public IActionResult UpdateMeasure(long measureId, [FromBody] Measure msr)
        {
            if (msr == null || msr.Id != measureId)
            {
                return BadRequest();
            }

            var measure = _dbContext.Measures
                .FirstOrDefault(m => m.Id == measureId);

            if (measure == null)
            {
                return NotFound();
            }

            measure.Name = msr.Name;

            _dbContext.Measures
                .Update(measure);

            _dbContext.SaveChanges();

            return new NoContentResult();
        }

        // DELETE api/measures/5
        [HttpDelete("{measureId}")]
        public IActionResult DeleteMeasure(int measureId)
        {
            var measure = _dbContext.Measures
                .FirstOrDefault(m => m.Id == measureId);

            if (measure == null)
            {
                return NotFound();
            }

            _dbContext.Measures
                .Remove(measure);

            _dbContext.SaveChanges();

            return new NoContentResult();
        }
    }
}
