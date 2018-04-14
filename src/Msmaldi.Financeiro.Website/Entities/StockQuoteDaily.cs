using System;
using Msmaldi.Financeiro.Website.Interfaces;

namespace Msmaldi.Financeiro.Website.Entities
{
    public class StockQuoteDaily : IStockQuote
    {
        public string Symbol { get; protected set; }
        public DateTime Date { get; protected set; }
        public double Open { get; protected set; }
        public double Close { get; protected set; }

        public StockQuoteDaily(string symbol, DateTime date, double open, double close)
        {
            Symbol = symbol;
            Date = date;
            Open = open;
            Close = close;
        }

        public StockQuoteDaily()
        {            
        }
    }
}