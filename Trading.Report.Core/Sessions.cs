using System.Collections.Generic;
using Trading.Core;

namespace Trading.Report.Core
{
    public class Session : Entity
    {
        public virtual IEnumerable<Trade> Trades { get; set; }
    }
}
