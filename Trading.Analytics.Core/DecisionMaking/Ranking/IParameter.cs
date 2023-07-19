using System;

namespace Trading.Researching.Core.DecisionMaking.Ranking
{
    public interface IParameter<TType, TValue> where TType : Enum
    {
        TType Type { get; }
        TValue Value { get; }
    }
}