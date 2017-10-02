using E_Saldytuvas.Server.Data;
using E_Saldytuvas.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Saldytuvas.Server.Services
{
    public class MeasureService : IMeasureService
    {
        private readonly ApplicationDbContext _dbContext;

        public MeasureService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Measure> GetMeasures()
        {
            var measures = _dbContext.Measures
                .ToList();

            return measures;
        }

        public Measure GetMeasure(int measureId)
        {
            var measure = _dbContext.Measures
                .FirstOrDefault(m => m.Id == measureId);

            return measure;
        }

        public bool AddMeasure(Measure measure)
        {
            if (measure == null)
            {
                return false;
            }

            _dbContext.Measures
                .Add(measure);

            _dbContext.SaveChanges();

            return true;
        }

        public int UpdateMeasure(long measureId, Measure msr)
        {
            if (msr == null || msr.Id != measureId)
            {
                return -1;
            }

            var measure = _dbContext.Measures
                .FirstOrDefault(m => m.Id == measureId);

            if (measure == null)
            {
                return -2;
            }

            measure.Name = msr.Name;

            _dbContext.Measures
                .Update(measure);

            _dbContext.SaveChanges();

            return 1;
        }

        public bool DeleteMeasure(int measureId)
        {
            var measure = _dbContext.Measures
                .FirstOrDefault(m => m.Id == measureId);

            if (measure == null)
            {
                return false;
            }

            _dbContext.Measures
                .Remove(measure);

            _dbContext.SaveChanges();

            return true;
        }
    }
}
