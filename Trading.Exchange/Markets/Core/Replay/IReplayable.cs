using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Markets.Core.Replay;

namespace Trading.Exchange.Markets.Core
{
    public interface IReplayable
    {
        IReplay GetReplay(DateTime from, DateTime to);
    }
}
