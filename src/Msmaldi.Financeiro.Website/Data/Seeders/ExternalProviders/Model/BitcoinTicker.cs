using System;

namespace Msmaldi.Financeiro.Website.Data.Seeders.ExternalProviders.Model
{
    public class BitcoinTradeTicker
    {
        public string message { get; set; }
        public BitcoinTradeTickerData data { get; set; }

        double Buy => data.buy;
        double Sell => data.sell;
        double Last => data.last;
    }

    public class BitcoinTradeTickerData
    {
        public double high { get; set; }
        public double low { get; set; }
        public double volume { get; set; }
        public double trades_quantity { get; set; }
        public double last { get; set; }
        public double sell { get; set; }
        public double buy { get; set; }
        public DateTime date { get; set; }
    }
}