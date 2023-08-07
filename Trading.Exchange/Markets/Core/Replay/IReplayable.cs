using Trading.Exchange.Markets.Core.Replay;

namespace Trading.Exchange.Markets.Core
{
    public interface IReplayable
    {
        IReplay GetReplay();
    }
}
