using System;

namespace Trading.Analytics.Core
{
    public interface IParameter<TType, TValue> where TType : Enum
    {
        TType Type { get; }
        TValue Value { get; }
    }
}