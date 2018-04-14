using System;

namespace Msmaldi.Financeiro.Website.Interfaces
{
    public interface IStockQuote
    {
        string Symbol { get; }
        DateTime Date { get; }
        double Close { get; }
    }
}