using Msmaldi.Financeiro.Website.Interfaces;

namespace Msmaldi.Financeiro.Website.BusinessLogic.SwingTrade
{
    public class PosicaoConsolidadaSwingTrade<TSwingTrade>
        where TSwingTrade : ISwingTrade
    {
        public TSwingTrade SwingTrade { get; }
        public double ValorDeAquisicao { get; }
        public double ValorBruto { get; }
        public double RendimentoBruto { get; }

        internal PosicaoConsolidadaSwingTrade(TSwingTrade swingTrade, double valorBruto)
        {
            SwingTrade = swingTrade;
            ValorDeAquisicao = swingTrade.ValorDeAquisicao * swingTrade.Quantidade;
            ValorBruto = valorBruto;
            RendimentoBruto = ValorBruto - ValorDeAquisicao;
        }
    }
}