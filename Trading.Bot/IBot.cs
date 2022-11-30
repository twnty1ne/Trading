using System;
using System.Collections.Generic;
using System.Text;
using Trading.Bot.Sessions;

namespace Trading.Bot
{
    public interface IBot
    {
        public ITradingSession Session { get; }
    }
}
