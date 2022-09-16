using System;

namespace Trading.Researching.Core
{
    public interface IParameter<TType, TValue> where TType : Enum
    {
        TType Type { get; }
        TValue Value { get; }
    }
}