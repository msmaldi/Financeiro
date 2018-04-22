using System.Linq;
using Msmaldi.Financeiro.Website.Interfaces;

namespace Msmaldi.Financeiro.Website.BusinessLogic.CryptoCurrencies
{
    public class PosicaoConsolidadaCryptoCurrencyFactory
    {
        public IQueryable<ICryptoCurrencyLastTicker> LastTicker { get; private set; }

        public PosicaoConsolidadaCryptoCurrencyFactory(IQueryable<ICryptoCurrencyLastTicker> cryptoCurrencyLastTickers)
        {
            LastTicker = cryptoCurrencyLastTickers;
        }

        public PosicaoConsolidadaCryptoCurrency<TCryptoWallet> ObterPosicaoConsolidada<TCryptoWallet>(
            TCryptoWallet cryptoWallet) where TCryptoWallet : ICryptoWallet
        {
            return new PosicaoConsolidadaCryptoCurrency<TCryptoWallet>(cryptoWallet, ValorBruto(cryptoWallet));
        }

        public double ValorBruto(ICryptoWallet cryptoWallet)
        {
            var ultimaCotacao = LastTicker.FirstOrDefault(c => c.CriptoCurrencyId == cryptoWallet.CriptoCurrencyId);
            if (ultimaCotacao == null)
                return cryptoWallet.Quantidade * cryptoWallet.ValorDeAquisicao;
            return cryptoWallet.Quantidade * ultimaCotacao.Last;
        }
    }
}