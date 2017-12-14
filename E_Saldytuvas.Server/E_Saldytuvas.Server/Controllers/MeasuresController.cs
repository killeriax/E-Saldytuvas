using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using E_Saldytuvas.Server.Data;
using E_Saldytuvas.Server.Models;
using E_Saldytuvas.Server.Services;

namespace E_Saldytuvas.Server.Controllers
{
    [Route("api/[controller]")]
    public class MeasuresController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly IMeasureService _measureService;

        public MeasuresController(ApplicationDbContext dbContext, IMeasureService measureService)
        {
            _dbContext = dbContext;
            _measureService = measureService;

            if (!_dbContext.Measures.Any())
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
            return _measureService.GetMeasures();
        }

        // GET api/measures/5
        [HttpGet("{measureId}", Name = "GetMeasure")]
        public IActionResult GetMeasure(int measureId)
        {
            var measure = _measureService.GetMeasure(measureId);

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
            if (_measureService.AddMeasure(measure))
            {
                return CreatedAtRoute("GetMeasure", new { measureId = measure.Id }, measure);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/measures/5
        [HttpPut("{measureId}")]
        public IActionResult UpdateMeasure(long measureId, [FromBody] Measure msr)
        {
            var result = _measureService.UpdateMeasure(measureId, msr);

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

        // DELETE api/measures/5
        [HttpDelete("{measureId}")]
        public IActionResult DeleteMeasure(int measureId)
        {
            if (_measureService.DeleteMeasure(measureId) == false)
            {
                return NotFound();
            }

            return new NoContentResult();
        }
    }
}
