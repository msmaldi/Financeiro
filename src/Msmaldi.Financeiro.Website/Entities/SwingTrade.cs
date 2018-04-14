using System;

namespace Msmaldi.Financeiro.Website.Entities
{
    public class SwingTrade
    {
        public long Id { get; protected set; }
        public string Symbol { get; protected set; }
        public int Quantidade { get; protected set; }
        public double ValorDeAquisicao { get; protected set; }

        public Guid ApplicationUserId { get; set; }
        public User ApplicationUser { get; set; }

        public Stock Stock { get; protected set; }

        public SwingTrade(string symbol, 
                             int quantidade, 
                          double valorDeAquisicao,
                            User applicationUser)
            : this(symbol, quantidade, valorDeAquisicao, applicationUser.Id)
        {
        }

        public SwingTrade(string symbol,
                             int quantidade,
                          double valorDeAquisicao,
                            Guid applicationUserId)
        {
            Symbol = symbol;
            Quantidade = quantidade;
            ValorDeAquisicao = valorDeAquisicao;
            ApplicationUserId = applicationUserId;
        }

        protected SwingTrade()
        {
        }
    }
}