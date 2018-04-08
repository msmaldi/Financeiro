using System.Linq;
using Msmaldi.Financeiro.Website.Interfaces;

namespace Msmaldi.Financeiro.Website.BusinessLogic.CDB
{
    public class PosicaoDetalhadaCDBComCDIFactory
    {
        private readonly IQueryable _taxasDIOver;
        public PosicaoDetalhadaCDBComCDIFactory(IQueryable<IDIOver> taxasDIOver)
        {
            _taxasDIOver = taxasDIOver;
        }

        
    }
}