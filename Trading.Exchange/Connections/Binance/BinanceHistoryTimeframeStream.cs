using System;
using System.Collections.Generic;
using Trading.Exchange.Connections.Chunks;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Connections.Binance
{
    internal class BinanceHistoryTimeframeStream : ITimeframeStream
    {
        private readonly IInstrumentName _name;
        private readonly Timeframes _timeframe;
        private readonly ICandleChunksPool _chunksPool;

        public BinanceHistoryTimeframeStream(IConnection connection, IInstrumentName name, Timeframes timeframe, IMarketTicker ticker)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _timeframe = timeframe;
            _chunksPool = new CandleChunksPool(timeframe, name, connection, ChunkSizes.Month, ticker);
            _chunksPool.OnNewChunk += HandleNewChunk;
            
            InitChunk(_chunksPool.First);
        }

        public event EventHandler<IReadOnlyCollection<ICandle>> OnCandleClosed;
        public event EventHandler<ICandle> OnCandleOpened;


        private void HandleNewChunk(object sender, ICandlesChunk nextChunk)
        {
            InitChunk(nextChunk);
        }

        private void InitChunk(ICandlesChunk chunk)
        {
            chunk.OnCandleClosed += (x, y) => OnCandleClosed?.Invoke(this, y);
            chunk.OnCandleOpened += (x, y) => OnCandleOpened?.Invoke(this, y);

            chunk.Load();
        }
    }
}