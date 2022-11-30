using System;
using Trading.Exchange.Markets.Core.Instruments.Positions;

namespace Trading.Exchange.Markets.Core.Instruments
{
    public interface IFuturesInstrument : IInstrument
    {
        event EventHandler<IPosition> OnPositionOpened;
        void SetPositionEntry(PositionSides side, int leverage, decimal stopLoss, decimal takeProfit, decimal size);
    }
}
