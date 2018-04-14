namespace Msmaldi.Financeiro.Website.Models.SwingTradeViewModels
{
    public class SwingTradeViewModel
    {
        public long Id { get; set; }
        public string Symbol { get; set; }
        public int QuantidadeDeAcoes { get; set; }
        public double ValorDeAquisicao { get; set; }
    }
}