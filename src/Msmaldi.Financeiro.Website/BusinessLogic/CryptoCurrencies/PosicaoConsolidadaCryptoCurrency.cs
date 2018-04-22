using Msmaldi.Financeiro.Website.Interfaces;

namespace Msmaldi.Financeiro.Website.BusinessLogic.CryptoCurrencies
{
    public class PosicaoConsolidadaCryptoCurrency<TCryptoWallet>
        where TCryptoWallet : ICryptoWallet
    {
        public TCryptoWallet CryptoWallet { get; }
        public double ValorDeAquisicao { get; }
        public double ValorBruto { get; }
        public double RendimentoBruto { get; }

        internal PosicaoConsolidadaCryptoCurrency(TCryptoWallet cryptoWallet, double valorBruto)
        {
            CryptoWallet = cryptoWallet;
            ValorDeAquisicao = cryptoWallet.ValorDeAquisicao; // * cryptoWallet.Quantidade;
            ValorBruto = valorBruto;
            RendimentoBruto = ValorBruto - ValorDeAquisicao;
        }
    }
}