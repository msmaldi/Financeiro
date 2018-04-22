using System;
using Msmaldi.Financeiro.Website.Interfaces;

namespace Msmaldi.Financeiro.Website.Entities
{
    public class CryptoCurrencyLastTicker : ICryptoCurrencyLastTicker
    {
        public string CriptoCurrencyId { get; protected set; }
        public CryptoCurrency CriptoCurrency { get; protected set; }
        public DateTime Date { get; set; }
        public double Last { get; set; }
        public double Buy { get; set; }
        public double Sell { get; set; }
        public string Source { get; set; }

        public CryptoCurrencyLastTicker(string criptoCurrencyId, DateTime date,
            double last, double buy, double sell, string source)
        {
            CriptoCurrencyId = criptoCurrencyId;
            Date = date;
            Last = last;
            Buy = buy;
            Sell = sell;
            Source = source;            
        }

        protected CryptoCurrencyLastTicker()
        {
        }
    }
}