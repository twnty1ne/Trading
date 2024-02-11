using Trading.Report.Core;

namespace Trading.Report.DAL
{
    public class TradeRepository : Repository<Trade>
    {
        public TradeRepository(SessionContext context) : base(context)
        {
        }
    }
}