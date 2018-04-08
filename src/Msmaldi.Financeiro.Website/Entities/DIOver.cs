using System;
using Msmaldi.Financeiro.Website.Interfaces;

namespace Msmaldi.Financeiro.Website.Entities
{
    public class DIOver : IDIOver
    {
        public DateTime Data { get; protected set; }
        public double Taxa { get; protected set; }

        protected DIOver()
        {
        }

        public DIOver(DateTime data, double taxa)
        {
            Data = data;
            Taxa = taxa;
        }
    }
}