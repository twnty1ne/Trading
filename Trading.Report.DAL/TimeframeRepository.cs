using Trading.Report.Core;

namespace Trading.Report.DAL
{
    public class TimeframeRepository : Repository<Timeframe>
    {
        public TimeframeRepository(SessionContext context) : base(context)
        {
        }
    }
}
