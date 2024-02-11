using System;
using System.Collections.Generic;
using System.Text;
using Trading.Shared.Resolvers;

namespace Trading.Shared.Ranges
{
    public class Range<T> : IRange<T> where T : IComparable<T>
    {
        public Range(T from, T to, BoundariesComparation boundariesComparation = BoundariesComparation.BothIncluding)
        {
            From = from ?? throw new ArgumentNullException(nameof(from));
            To = to ?? throw new ArgumentNullException(nameof(to));
            BoundariesComparation = boundariesComparation;
            if (from.CompareTo(to) > 0) throw new ArgumentException();
        }

        public T From { get; }
        public T To { get; }

        public BoundariesComparation BoundariesComparation { get; }

        public bool Contains(T value) 
        {
            if(BoundariesComparation == BoundariesComparation.BothIncluding)
                return From.CompareTo(value) <= 0 && To.CompareTo(value) >= 0; 
            
            if(BoundariesComparation == BoundariesComparation.BothExcluding)
                return From.CompareTo(value) < 0 && To.CompareTo(value) > 0; 
            
            if(BoundariesComparation == BoundariesComparation.LeftIncluding)
                return From.CompareTo(value) <= 0 && To.CompareTo(value) > 0; 
            
            if(BoundariesComparation == BoundariesComparation.RightIncluding)
                return From.CompareTo(value) < 0 && To.CompareTo(value) >= 0;

            throw new ArgumentOutOfRangeException();
        }
    }
}
