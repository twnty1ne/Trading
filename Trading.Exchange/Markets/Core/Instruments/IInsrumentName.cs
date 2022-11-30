using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Markets.Core.Instruments
{
    public interface IInstrumentName
    {
        public string BaseCurrencyName { get;}
        public string QuoteCurrencyName { get; }

        public string GetFullName();
        public string GetFullName(string separator);

    }
}
