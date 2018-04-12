using System.Collections.Generic;

namespace Msmaldi.Financeiro.Website.Entities
{
    public class Stock
    {
        public string Symbol { get; protected set; }
        public string Currency { get; protected set; }

        public IEnumerable<StockQuoteDaily> StockQuotesDaily { get; protected set; }
    }
}