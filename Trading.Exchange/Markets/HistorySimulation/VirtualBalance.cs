using System;
using System.Diagnostics;
using Trading.Exchange.Markets.Core;

namespace Trading.Exchange.Markets.HistorySimulation
{
    internal class VirtualBalance : IBalance, IVirtualBalance
    {
        private object _lock = new object();
        private decimal _allocatedVolume = 0m;

        public VirtualBalance(decimal netVolume)
        {
            NetVolume = netVolume;
        }

        public decimal NetVolume { get; private set; }

        public decimal CurrentVolume { get => NetVolume - _allocatedVolume; }

        public void Allocate(decimal volume)
        {
            lock (_lock)
            {
                if (volume > NetVolume - _allocatedVolume) throw new ArgumentOutOfRangeException();
                if (volume < 0) throw new ArgumentOutOfRangeException();
                
                _allocatedVolume += volume;
                Debug.WriteLine($"Allocated: {volume}");
            } 
        }

        public void Release(decimal volume)
        {
            lock (_lock)
            {
                if (volume < 0) throw new ArgumentOutOfRangeException();
                if(volume > _allocatedVolume) throw new ArgumentOutOfRangeException();
                
                _allocatedVolume -= volume;
                Debug.WriteLine($"Released: {volume}");
            }
        }

        public decimal GetNetVolumePercent(decimal percent)
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
