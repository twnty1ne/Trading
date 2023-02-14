using System;
using Trading.Exchange.Connections;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Markets.HistorySimulation
{
    internal class HistorySimulationFuturesInstrument : IFuturesInstrument
    {
        private readonly IConnection _connection;
        private readonly IFuturesInstrument _instrument;
        private readonly VirtualBalance _balance;

        public HistorySimulationFuturesInstrument(IInstrumentName name, IConnection connection, IMarketTicker ticker, VirtualBalance balance)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _balance = balance ?? throw new ArgumentNullException(nameof(balance));
            _ = name ?? throw new ArgumentNullException(nameof(name));
            _instrument = new FuturesInstrument(name, _connection.GetHistoryInstrumentStream(name, ticker), _connection);
            _instrument.OnPriceUpdated += HandlePriceUpdated;
            _instrument.OnPositionOpened += HandlePositionOpened;
        }

        public event EventHandler<IPriceTick> OnPriceUpdated;
        public event EventHandler<IPosition> OnPositionOpened;

        public IInstrumentName Name { get => _instrument.Name; }

        public decimal Price { get => _instrument.Price; }

        public ITimeframe GetTimeframe(Timeframes type)
        {
            return _instrument.GetTimeframe(type);
        }

        private void HandlePriceUpdated(object sender, IPriceTick tick) 
        {
            OnPriceUpdated?.Invoke(sender, tick);
        }

        private void HandlePositionOpened(object sender, IPosition position)
        {
            position.OnClosed += (x, y) => 
            {
                _balance.Release(position.InitialMargin);
                _balance.Update(position.RealizedPnl);

            };
            OnPositionOpened?.Invoke(sender, position);
        }

        public void SetPositionEntry(PositionSides side, int leverage, decimal stopLoss, decimal takeProfit, decimal size, Guid id)
        {
            var v = size * Price / leverage;
            _balance.Allocate(v);
            _instrument.SetPositionEntry(side, leverage, stopLoss, takeProfit, size, id);
        }
    }
}
