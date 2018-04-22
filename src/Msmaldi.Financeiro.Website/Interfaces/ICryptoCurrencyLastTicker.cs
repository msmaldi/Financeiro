using System;

namespace Msmaldi.Financeiro.Website.Interfaces
{
    public interface ICryptoCurrencyLastTicker
    {
        string CriptoCurrencyId { get; }
        DateTime Date { get; }
        double Last { get; }
        double Buy { get; }
        double Sell { get; }
    }
}