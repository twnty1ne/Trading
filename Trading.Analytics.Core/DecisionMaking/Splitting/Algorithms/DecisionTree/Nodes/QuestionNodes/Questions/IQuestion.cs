namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions
{
    public interface IQuestion<TItem>
    {
        SplitAnswer Ask(TItem item);
    }
}