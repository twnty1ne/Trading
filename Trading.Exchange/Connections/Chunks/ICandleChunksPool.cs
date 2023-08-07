using System;

namespace Trading.Exchange.Connections.Chunks
{
    public interface ICandleChunksPool
    {
        event EventHandler<ICandlesChunk> OnNewChunk;
        ICandlesChunk First { get; }
    }
}