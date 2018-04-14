namespace Msmaldi.Financeiro.Website.Interfaces
{
    public interface ISwingTrade
    {
        string Symbol { get; }
        int Quantidade { get; }
        double ValorDeAquisicao { get; }
    }
}