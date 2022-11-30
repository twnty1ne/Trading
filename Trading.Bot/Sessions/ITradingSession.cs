using System;

namespace Trading.Bot.Sessions
{
    public interface ITradingSession
    {
        event EventHandler<ISessionBuffer> OnStopped;
        DateTime Date { get; }
        void Start();
        void Stop();
    }
}
