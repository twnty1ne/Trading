using System;
using System.Collections.Generic;
using System.Linq;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Shared.Ranges;
using Trading.Shared.Ranges.Extensions;

namespace Trading.Exchange.Connections.Chunks
{
    internal class CandleChunksPool : ICandleChunksPool
    {
        private readonly Timeframes _timeframe;
        private readonly IInstrumentName _instrumentName;
        private readonly IConnection _connection;
        private readonly IRange<DateTime> _poolRange;
        private readonly ChunkSizes _chunkSize;
        private readonly IMarketTicker _ticker;
        private readonly LinkedList<CandlesChunk> _chunks;

        public event EventHandler<ICandlesChunk> OnNewChunk;

        public ICandlesChunk First => _chunks.First();

        public CandleChunksPool(Timeframes timeframe, IInstrumentName instrumentName, IConnection connection, ChunkSizes chunkSize, 
            IMarketTicker ticker)
        {
            _timeframe = timeframe;
            _chunkSize = chunkSize;
            _ticker = ticker ?? throw new ArgumentNullException(nameof(ticker));
            _instrumentName = instrumentName ?? throw new ArgumentNullException(nameof(instrumentName));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _poolRange = _ticker.TicksRange;
            _chunks = CreateChunks();
        }

        private LinkedList<CandlesChunk> CreateChunks()
        {
            var chunks = GetDateTimeChunks();
            
            var candlesChunk = chunks.Select(x =>
            {
                var chunk =  new CandlesChunk(_connection, _instrumentName, _timeframe, _ticker, TimeSpan.FromDays(2), _chunkSize, x);
                
                chunk.OnChunkDone += (y, z) =>
                {
                    if(!LoadNext(chunk))
                            return;
                        
                    var nextChunk = GetNextChunk(chunk);
                    nextChunk.LoadIntersection(z);
                
                    OnNewChunk?.Invoke(this, nextChunk);
                };
                
                return chunk;
            });

            return new LinkedList<CandlesChunk>(candlesChunk);
        }
        
        private IEnumerable<IRange<DateTime>> GetDateTimeChunks()
        {
            if(_chunkSize == ChunkSizes.Month) return _poolRange.MonthChunks();
            
            throw new ArgumentOutOfRangeException();
        }

        private CandlesChunk GetNextChunk(CandlesChunk chunk)
        {
            var currentChunkNode = _chunks.Find(chunk);

            if (currentChunkNode?.Next is null)
                throw new ArgumentOutOfRangeException();
            
            var nextChunk =  currentChunkNode.Next.Value;
            _chunks.Remove(chunk);

            return nextChunk;
        }

        private bool LoadNext(CandlesChunk chunk)
        {
            return _chunks.Find(chunk)?.Next?.Value != null;
        }
    }
}