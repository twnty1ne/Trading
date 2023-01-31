using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Report.Core;
using Trading.Report.DAL;

namespace Trading.Report.DAL
{
    public class SessionRepository : Repository<Session>
    {
        public SessionRepository(SessionContext context) : base(context)
        {
        }
    }
}
