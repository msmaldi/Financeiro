namespace Msmaldi.Financeiro.Website.Entities
{
    public class CryptoCurrency
    {
        public string Id { get; protected set; }
        public string Name { get; protected set; }

        public CryptoCurrencyLastTicker LastTicker { get; set; }

        public CryptoCurrency(string symbol, string name)
        {
            Id = symbol;
            Name = name;
        }

        protected CryptoCurrency()
        {            
        }
    }
}