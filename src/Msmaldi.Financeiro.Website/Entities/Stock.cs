using System.Collections.Generic;

namespace Msmaldi.Financeiro.Website.Entities
{
    public class Stock
    {
        public string Symbol { get; protected set; }
        public string Currency { get; protected set; }

        public IEnumerable<StockQuoteDaily> StockQuotesDaily { get; protected set; }

        public Stock(string symbol, string currenty = "BRL")
        {
            Symbol = symbol;
            Currency = currenty;
        }

        public Stock()
        {            
        }
    }
}