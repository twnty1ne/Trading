using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Shared.Resolvers
{
    public interface IResolver<T, R> where T : Enum
    {
        R Resolve(T type);
        bool TryResolve(T type, out R item);
    }
}
