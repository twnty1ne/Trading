using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Markets.HistorySimulation
{
    internal interface IVirtualBalance
    {
        void Update(decimal value);
        void Allocate(decimal volume);
        
    }
}
