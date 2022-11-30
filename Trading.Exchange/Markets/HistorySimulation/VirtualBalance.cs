using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Markets.Core;

namespace Trading.Exchange.Markets.HistorySimulation
{
    internal class VirtualBalance : IBalance, IVirtualBalance
    {
        private object _lock = new object();
        public VirtualBalance(decimal netVolume)
        {
            NetVolume = netVolume;
        }

        public decimal NetVolume { get; private set; }

        public decimal GetPercent(decimal percent)
        {
            lock (_lock)
            {
                return NetVolume * percent;
            }
        }

        public void Update(decimal value)
        {
            lock (_lock) 
            {
                if (value < 0 && value > NetVolume) throw new ArgumentOutOfRangeException();
                NetVolume += value;
            }
           
        }
    }
}
