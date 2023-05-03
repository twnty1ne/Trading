using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Core;

namespace Trading.Report.Core
{
    public class Session : Entity
    {
        public virtual IEnumerable<Trade> Trades { get; set; }
    }
}
