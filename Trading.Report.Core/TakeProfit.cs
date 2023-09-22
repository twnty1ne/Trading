using Trading.Core;

namespace Trading.Report.Core;

public class TakeProfit : Entity
{
    public decimal Price { get; set; }
    public decimal Volume { get; set; }
}