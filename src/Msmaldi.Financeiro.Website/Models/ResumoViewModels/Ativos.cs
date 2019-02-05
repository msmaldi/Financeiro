using System.Collections.Generic;
using System.Linq;
using Msmaldi.Financeiro.Website.BusinessLogic.CDB;
using Msmaldi.Financeiro.Website.BusinessLogic.CryptoCurrencies;
using Msmaldi.Financeiro.Website.BusinessLogic.SwingTrade;
using Msmaldi.Financeiro.Website.Entities;

namespace Msmaldi.Financeiro.Website.Models.ResumoViewModels
{
    public class Ativos
    {
        public IEnumerable<PosicaoConsolidadaCDBComCDI<CDBComCDI>> PosicoesConsolidadasCDBComCDI { get; }
        public IEnumerable<PosicaoConsolidadaSwingTrade<SwingTrade>> PosicoesConsolidadasSwingTrade { get; }
        public IEnumerable<PosicaoConsolidadaCryptoCurrency<CryptoWallet>> PosicoesConsolidadasCryptoWallet { get; }

        public double TotalCDIeSELIC { get; }
        public double TotalCDIeSELICPorcent { get; }

        public double TotalRendaVariavel { get; }
        public double TotalRendaVariavelPorcent { get; }

        public double TotalCryptoCurrency { get; }
        public double TotalCryptoCurrencyPorcent { get; }

        public Ativos(
            User user,
            PosicaoConsolidadaCDBComCDIFactory cdbFactory,
            PosicaoConsolidadaSwingTradeFactory swingTradeFactory,
            PosicaoConsolidadaCryptoCurrencyFactory cryptoCurrencyFactory
            )
        {
            PosicoesConsolidadasCDBComCDI =
                user.CDBsComCDI.Select(cdb => cdbFactory.ObterPosicaoConsolidada(cdb)).ToList();

            PosicoesConsolidadasSwingTrade =
                user.SwingTrades.Select(st => swingTradeFactory.ObterPosicaoConsolidada(st)).ToList();

            PosicoesConsolidadasCryptoWallet =
                user.CryptoWallets.Select(c => cryptoCurrencyFactory.ObterPosicaoConsolidada(c)).ToList();

            TotalCDIeSELIC = PosicoesConsolidadasCDBComCDI.Sum(cdb => cdb.ValorLiquido);

            TotalRendaVariavel = PosicoesConsolidadasSwingTrade.Sum(x => x.ValorBruto);

            TotalCryptoCurrency = PosicoesConsolidadasCryptoWallet.Sum(b => b.ValorBruto);


            TotalCDIeSELICPorcent = TotalCDIeSELIC / ValorBruto();
            TotalRendaVariavelPorcent = TotalRendaVariavel / ValorBruto();
            TotalCryptoCurrencyPorcent = TotalCryptoCurrency / ValorBruto();
        }

        public double ValorBruto()
        {
            return TotalCDIeSELIC + TotalRendaVariavel + TotalCryptoCurrency;
        }

        public double SalarioMinimo => 998.0;

        public double QuantidadeDeSalariosMinimos => ValorBruto() / SalarioMinimo;
    }
}