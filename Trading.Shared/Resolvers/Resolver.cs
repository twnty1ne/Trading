using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Shared.Resolvers
{
    public class Resolver<T, R> : IResolver<T, R> where T : Enum
    {

        private readonly Dictionary<T, Func<R>> _dictionary;

        public Resolver(Dictionary<T, Func<R>> dictionary)
        {
            _dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }

        public R Resolve(T type)
        {
            var resolveFunction = _dictionary.GetValueOrDefault(type);
            if (resolveFunction is null) throw  new NotSupportedException($"specified item is not supported. item: {type}");
            return _dictionary.GetValueOrDefault(type).Invoke();
        }

        public bool TryResolve(T type, out R item)
        {
            Func<R> resolverFunction;
            var success = _dictionary.TryGetValue(type, out resolverFunction);
            item = success ? resolverFunction.Invoke() : default(R);
            return success;
        }
    }
}
