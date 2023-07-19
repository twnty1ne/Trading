namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions.Features
{
    public interface IFeature<TItem, T> where TItem : class
    {
        T GetValue(TItem item);
    }
}