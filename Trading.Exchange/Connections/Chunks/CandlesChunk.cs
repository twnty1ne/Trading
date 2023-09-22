using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Stateless;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Exchange.Markets.Core.Instruments.Timeframes.Extentions;
using Trading.Shared.Ranges;

namespace Trading.Exchange.Connections.Chunks
{
    internal class CandlesChunk : ICandlesChunk                                                                                                          
    {
        private readonly TimeSpan _chunksIntersectionSpan;
        private readonly IInstrumentName _name;                                                                                           
        private readonly Timeframes _timeframe;                                                                                           
        private readonly long _timeframeTicks;                                                                                            
        private readonly IMarketTicker _ticker;
        private readonly List<ICandle> _intersectionCandles = new();
        private Dictionary<long, ICandle> _candleBuffer;                                                                                  
        private IReadOnlyCollection<ICandle> _closedCandles = new List<ICandle>();                                                                              
        private readonly IConnection _connection;
        private readonly ChunkSizes _chunkSize;
        private readonly object _lock = new object();
        private readonly StateMachine<CandlesChunkStates, CandlesChunkTriggers> _stateMachine = 
            new(CandlesChunkStates.WaitingForLoad, FiringMode.Immediate);

        public event EventHandler<IEnumerable<ICandle>> OnChunkDone;                                                                              
        public event EventHandler<IReadOnlyCollection<ICandle>> OnCandleClosed;                                                           
        public event EventHandler<ICandle> OnCandleOpened;
        
        public IRange<DateTime> Range { get; }
        
                                                                                                                                          
        public CandlesChunk(IConnection connection, IInstrumentName name, Timeframes timeframe, IMarketTicker ticker, 
            TimeSpan chunksIntersectionSpan, ChunkSizes chunkSize, IRange<DateTime> chunkRange)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _ticker = ticker ?? throw new ArgumentNullException(nameof(ticker));
            _chunksIntersectionSpan = chunksIntersectionSpan;
            _chunkSize = chunkSize;
            _timeframe = timeframe;
            _timeframeTicks = _timeframe.GetTimeframeTimeSpan().Ticks;
            Range = chunkRange ?? throw new ArgumentNullException(nameof(chunkRange));

            _stateMachine
                .Configure(CandlesChunkStates.WaitingForLoad)
                .Permit(CandlesChunkTriggers.Load, CandlesChunkStates.Loaded);

            _stateMachine
                .Configure(CandlesChunkStates.Loaded)
                .OnEntryFrom(CandlesChunkTriggers.Load, HandleLoad)
                .Permit(CandlesChunkTriggers.Done, CandlesChunkStates.Done);

            _stateMachine
                .Configure(CandlesChunkStates.Done)
                .OnEntryFrom(CandlesChunkTriggers.Done, HandleDone);
        }                                                                                                                                 
     
        private void HandleNewTick(object sender, IMarketTick tick)                                                                       
        {
            if (tick.Date > Range.To)                                                                                              
            {
                _stateMachine.Fire(CandlesChunkTriggers.Done);
                return;
            }  
            
            var closedCandle = _candleBuffer.GetValueOrDefault(tick.Date.Ticks);                                                          
            var openedCandle = _candleBuffer.GetValueOrDefault(tick.Date.Ticks + _timeframeTicks);                                        
                                                                                                                                          
            if (closedCandle != null)                                                                                                     
            {                                                                                                                             
                var closedCandlesCopy = new List<ICandle>(_closedCandles) { closedCandle };
                _closedCandles = closedCandlesCopy;                                                                                       
                OnCandleClosed?.Invoke(this, _closedCandles);                                                                             
            }                                                                                                                             
     
            if (openedCandle != null)                                                                                                     
            {                                                                                                                             
                OnCandleOpened?.Invoke(this, openedCandle);                                                                               
            }
        }                                                                                                                                 
     
        public void Load()                                                                                                          
        {
            _stateMachine.Fire(CandlesChunkTriggers.Load);
        }

        // Temporary not clean method till we change file storage 
        public void LoadIntersection(IEnumerable<ICandle> candles)
        {
            _intersectionCandles.AddRange(candles);
        }

        private void HandleLoad()
        {
            Debug.WriteLine($"Loading chunk  for {_name.GetFullName()} {_timeframe.ToString()}");
                
            var candles = _connection.GetFuturesCandlesAsync(_name, _timeframe, Range).GetAwaiter().GetResult();
            var allCandles = _intersectionCandles.Concat(candles);
            _intersectionCandles.Clear();

            _candleBuffer = allCandles.ToDictionary(x => x.CloseTime.Ticks + TimeSpan.FromSeconds(1).Ticks);
            
            Debug.WriteLine($"Chunk for {_name.GetFullName()} {_timeframe.ToString()} has been loaded");

            _ticker.OnTick += HandleNewTick; 
        }

        private void HandleDone()
        {
            _ticker.OnTick -= HandleNewTick;
                
            Debug.WriteLine($"Chunk for {_name.GetFullName()} {_timeframe.ToString()} is done");

            var candlesInIntersectionSpan =  (int)(_chunksIntersectionSpan.Ticks / _timeframeTicks);
            var intersectionCandles = _closedCandles.TakeLast(candlesInIntersectionSpan);
            OnChunkDone?.Invoke(this, intersectionCandles);                                                                                        
     
            _closedCandles = null;                                                                                                    
            _candleBuffer.Clear();
            
            OnChunkDone = null;                                                                                                       
            OnCandleClosed = null;                                                                                                    
            OnCandleOpened = null;
        }
    }                                                                                                                                     
}