using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Researching.Core.Analytics
{
    public class Selection<TParameter, TSelection> : ISelection<TParameter, TSelection>
        where TParameter : Enum
    {
        public Selection(IReadOnlyCollection<IParameter<TParameter, decimal>> parameters, IReadOnlyCollection<TSelection> data)
        {
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public IReadOnlyCollection<IParameter<TParameter, decimal>> Parameters { get; private set; }

        public IReadOnlyCollection<TSelection> Data { get; private set; }
    }

    public class Selection<TSelection> : ISelection<TSelection>
    {
        public Selection(IReadOnlyCollection<TSelection> data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public IReadOnlyCollection<TSelection> Data { get; private set; }
    }




}
