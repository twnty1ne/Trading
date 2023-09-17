namespace Trading.Bot.Strategies;

public interface IContextParser<out TContext> where TContext : class
{
    TContext Parse(ISignal signal);
}