using System;
using Msmaldi.Financeiro.Website.Interfaces;

namespace Msmaldi.Financeiro.Website.Entities
{
    public class CryptoWallet : ICryptoWallet
    {

        public CryptoWallet(string label, double quantidade, double valorDeAquisicao, string cryptoCurrencyId, Guid userId)
        {
            Label = label;
            Quantidade = quantidade;
            ValorDeAquisicao = valorDeAquisicao;
            CriptoCurrencyId = cryptoCurrencyId;
            UserId = userId;
        }

        protected CryptoWallet()
        {
        }

        public long Id { get; protected set; }
        public string Label { get; protected set; }
        public double Quantidade { get; protected set; }
        public double ValorDeAquisicao { get; protected set; }

        public string CriptoCurrencyId { get; protected set; }
        public CryptoCurrency CriptoCurrency { get; protected set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}