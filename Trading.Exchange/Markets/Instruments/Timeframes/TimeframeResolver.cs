using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trading.Exchange.Connections;
using Trading.Shared.Resolvers;
using Microsoft.Extensions.Caching.Memory;

namespace Trading.Exchange.Markets.Instruments.Timeframes
{
    internal class TimeframeResolver : IResolver<Timeframes, ITimeframe>
    {
        private readonly IInstrumentName _instrumentName;
        private readonly IInstrumentSocketConnection _socketConnection;
        private readonly IMemoryCache _timeframes;
        private readonly IResolver<Timeframes, ITimeframe> _resolver;
        private readonly IConnection _connection;

        public TimeframeResolver(IInstrumentName instrumentName, IInstrumentSocketConnection socketConnection, IConnection connection)
        {
            _instrumentName = instrumentName ?? throw new ArgumentNullException(nameof(instrumentName));
            _socketConnection = socketConnection ?? throw new ArgumentNullException(nameof(socketConnection));
            _resolver = new Resolver<Timeframes, ITimeframe>(CreateDictionary());
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _timeframes = new MemoryCache(new MemoryCacheOptions());
        }

        public ITimeframe Resolve(Timeframes type)
        {
            return _resolver.Resolve(type);
        }

        public bool TryResolve(Timeframes type, out ITimeframe item)
        {
            return _resolver.TryResolve(type, out item);
        }

        private Dictionary<Timeframes, Func<ITimeframe>> CreateDictionary() 
        {

            return new Dictionary<Timeframes, Func<ITimeframe>>
            {
                { Timeframes.FourHours, () => GetOrCreate(Timeframes.FourHours) },
                { Timeframes.OneDay, () => GetOrCreate(Timeframes.OneDay) },
                { Timeframes.ThirtyMinutes, () => GetOrCreate(Timeframes.ThirtyMinutes) },
                { Timeframes.OneHour, () => GetOrCreate(Timeframes.OneHour) },
                { Timeframes.FiveMinutes, () => GetOrCreate(Timeframes.FiveMinutes) },
            };
        }

        private ITimeframe GetOrCreate(Timeframes timeframe) 
        {
            return _timeframes.GetOrCreate(timeframe, x => new Timeframe(_instrumentName, _connection, _socketConnection, timeframe));
        }
    }
}
