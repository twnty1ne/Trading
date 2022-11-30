using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Bot.Sessions
{
    internal enum SessionStates
    {
        WaitingForStart = 1,
        Started = 2,
        Stopped = 3
    }
}
