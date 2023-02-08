using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Shared.Ranges
{
    public interface IRange<T> where T : IComparable<T>
    {
        T From { get; }
        T To { get; }
        bool Contains(T value);
    }
}
