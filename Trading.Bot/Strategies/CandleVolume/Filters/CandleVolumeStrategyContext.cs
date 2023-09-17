using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions.Features.Attributes;

namespace Trading.Bot.Strategies.CandleVolume.Filters
{
    public class CandleVolumeStrategyContext
    {
        [Feature("pd_size", FeatureType.Continuous)]
        public required decimal PdSize { get; set; }
        
        [Feature("day_time", FeatureType.Continuous)]
        public required int DayTime { get; set; }
        
        [Feature("short", FeatureType.Bool)]
        public required bool Short { get; set; }
        
        [Feature("take_profit_channel_extension", FeatureType.Continuous)]
        public required decimal TakeProfitChannelExtension { get; set; }
        
        [Feature("equilibrium_distance", FeatureType.Bool)]
        public required decimal EquilibriumDistance { get; set; }
        
    }
}