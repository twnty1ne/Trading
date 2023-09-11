using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions.Features.Attributes;

namespace Trading.Api
{
    public class CandleVolumeStrategyContext
    {
        public CandleVolumeStrategyContext(decimal pdSize, decimal dayTime, bool @short)
        {
            PdSize = pdSize;
            DayTime = dayTime;
            Short = @short;
        }

        [Feature("pd_size", FeatureType.Continuous)]
        public decimal PdSize { get; }
        
        [Feature("day_time", FeatureType.Continuous)]
        public decimal DayTime { get; }
        
        [Feature("short", FeatureType.Bool)]
        public bool Short { get; }
        
        [Feature("take_profit_channel_extension", FeatureType.Continuous)]
        public bool TakeProfitChannelExtention { get; }
        
        [Feature("equlibrium_distance", FeatureType.Bool)]
        public bool EqulibriumDistance { get; }
        
    }
}