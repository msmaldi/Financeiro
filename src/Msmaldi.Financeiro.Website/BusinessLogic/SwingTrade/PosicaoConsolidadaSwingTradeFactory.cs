using System.Linq;
using Msmaldi.Financeiro.Website.Interfaces;

namespace Msmaldi.Financeiro.Website.BusinessLogic.SwingTrade
{
    public class PosicaoConsolidadaSwingTradeFactory
    {
        public IQueryable<IStockQuote> StockQuotes { get; private set; }

        public PosicaoConsolidadaSwingTradeFactory(IQueryable<IStockQuote> stockQuotes)
        {
            StockQuotes = stockQuotes;
        }

        public PosicaoConsolidadaSwingTrade<TSwingTrade> ObterPosicaoConsolidada<TSwingTrade>(
            TSwingTrade swingTrade) where TSwingTrade : ISwingTrade
        {
            return new PosicaoConsolidadaSwingTrade<TSwingTrade>(swingTrade, ValorBruto(swingTrade));
        }

        public double ValorBruto(ISwingTrade swingTrade)
        {
            var ultimaCotacao = StockQuotes.LastOrDefault(s => s.Symbol == swingTrade.Symbol);
            if (ultimaCotacao == null)
                return swingTrade.Quantidade * swingTrade.ValorDeAquisicao;
            return swingTrade.Quantidade * ultimaCotacao.Close;
        }
    }
}