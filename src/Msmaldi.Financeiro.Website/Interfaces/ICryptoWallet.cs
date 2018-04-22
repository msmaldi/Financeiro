using Msmaldi.Financeiro.Website.Entities;

namespace Msmaldi.Financeiro.Website.Interfaces
{
    public interface ICryptoWallet
    {
        double Quantidade { get; }
        double ValorDeAquisicao { get; }
        string CriptoCurrencyId { get; }
    }
}