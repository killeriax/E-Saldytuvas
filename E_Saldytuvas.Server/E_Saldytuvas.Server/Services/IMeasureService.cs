using E_Saldytuvas.Server.Models;
using System.Collections.Generic;

namespace E_Saldytuvas.Server.Services
{
    public interface IMeasureService
    {
        IEnumerable<Measure> GetMeasures();
        Measure GetMeasure(int measureId);
        bool AddMeasure(Measure measure);
        int UpdateMeasure(long measureId, Measure msr);
        bool DeleteMeasure(int measureId);
    }
}
