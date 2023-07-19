using System;

namespace Trading.Researching.Core.DecisionMaking.Ranking
{
    public class Parameter<TType, TValue> : IParameter<TType, TValue> where TType : Enum
    {
        public Parameter(TType type, TValue value)
        {
            Type = type;
            Value = value;
        }

        public TType Type { get; private set; }
        public TValue Value { get; private set; }
    }
}
