namespace Trading.Exchange.Connections.Chunks
{
    internal enum CandlesChunkStates
    {
        WaitingForLoad = 1,
        Loaded = 2,
        Done = 3
    }
}