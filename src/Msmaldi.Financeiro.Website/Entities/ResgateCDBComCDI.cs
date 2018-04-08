using System;
using Msmaldi.Financeiro.Website.Interfaces;

namespace Msmaldi.Financeiro.Website.Entities
{
    public class ResgateCDBComCDI : IResgateCDI
    {
        public int CDBComCDIId { get; set; }
        public DateTime Data { get; set; }
        public int Quantidade { get; set; }
    }
}