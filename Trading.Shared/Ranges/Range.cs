using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Shared.Ranges
{
    public class Range<T> : IRange<T> where T : IComparable<T>
    {
        public Range(T from, T to)
        {
            From = from ?? throw new ArgumentNullException(nameof(from));
            To = to ?? throw new ArgumentNullException(nameof(to));
            if (from.CompareTo(to) > 0) throw new ArgumentException();
        }

        public T From { get; }
        public T To { get; }

        public bool Contains(T value) 
        {
            return From.CompareTo(value) <= 0 && To.CompareTo(value) >= 0; 
        }
    }
}
