using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestService.Models;

namespace TestService.Service
{
    public interface IInitialData
    {
        public Task<IEnumerable<Conductance>> GetConductantList(long accountId, PeriodType periodType);
    }
}
