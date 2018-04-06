using System;

namespace Msmaldi.Financeiro.Website.Entities
{
    public class DIOver
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