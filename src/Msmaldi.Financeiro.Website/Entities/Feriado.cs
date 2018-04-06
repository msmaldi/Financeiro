using System;

namespace Msmaldi.Financeiro.Website.Entities
{
    public class Feriado
    {
        public DateTime Data { get; protected set; }
        public string Nome { get; protected set; }

        protected Feriado()
        {
        }

        public Feriado(DateTime data, string nome)
        {
            Data = data;
            Nome = nome;
        }
    }
}