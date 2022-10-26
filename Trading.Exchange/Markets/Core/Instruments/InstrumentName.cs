using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Markets.Core.Instruments
{
    public class InstrumentName : IInstrumentName
    {
        public InstrumentName(string baseCurrencyName, string quoteCurrencyName)
        {
            BaseCurrencyName = baseCurrencyName ?? throw new ArgumentNullException(nameof(baseCurrencyName));
            QuoteCurrencyName = quoteCurrencyName ?? throw new ArgumentNullException(nameof(quoteCurrencyName));
        }

        public string BaseCurrencyName { get; private set; }
        public string QuoteCurrencyName { get; private set; }

        public string GetFullName()
        {
            return GetFullName(string.Empty);
        }

        public string GetFullName(string separator)
        {
            return $"{BaseCurrencyName}{separator}{QuoteCurrencyName}";
        }
    }
}
