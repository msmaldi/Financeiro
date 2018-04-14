using System;

namespace Msmaldi.Financeiro.Website.Entities
{
    public class SwingTrade
    {
        public long Id { get; protected set; }
        public string Symbol { get; protected set; }
        public int Quantidade { get; protected set; }
        public double ValorDeAquisicao { get; protected set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Stock Stock { get; protected set; }

        public SwingTrade(string symbol, 
                             int quantidade, 
                          double valorDeAquisicao,
                            User user)
            : this(symbol, quantidade, valorDeAquisicao, user.Id)
        {
        }

        public SwingTrade(string symbol,
                             int quantidade,
                          double valorDeAquisicao,
                            Guid userId)
        {
            Symbol = symbol;
            Quantidade = quantidade;
            ValorDeAquisicao = valorDeAquisicao;
            UserId = userId;
        }

        protected SwingTrade()
        {
        }
    }
}