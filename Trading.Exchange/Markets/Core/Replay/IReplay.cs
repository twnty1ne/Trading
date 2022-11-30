using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Markets.Core.Replay
{
    public interface IReplay
    {
        event EventHandler OnStarted;
        event EventHandler OnDone;

        void Start();
        void Stop();

    }
}
