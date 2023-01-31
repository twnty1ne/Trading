using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Report.Core;

namespace Trading.Report.DAL
{
    public class StrategyRepository : Repository<Strategy>
    {
        public StrategyRepository(SessionContext context) : base(context)
        {
        }
    }
}
