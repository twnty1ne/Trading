using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Bot
{
    public class Options
    {
        public Sessions.Sessions Session { get; set; }
        public Strategies.Strategies Strategy { get; set; } 
    }
}
