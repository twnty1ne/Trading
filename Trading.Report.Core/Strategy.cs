using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Bot.Strategies;
using Trading.Core;

namespace Trading.Report.Core
{
    public class Strategy : Entity
    {
        public string Name { get; set; }
        public Strategies Type { get; set; }
    }
}
