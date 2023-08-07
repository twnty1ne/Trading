using System;
using Trading.Shared.Ranges;

namespace Trading.Exchange
{
    public class HistorySimulationOptions
    {
        public IRange<DateTime> SimulationRange { get; set; }
    }
}