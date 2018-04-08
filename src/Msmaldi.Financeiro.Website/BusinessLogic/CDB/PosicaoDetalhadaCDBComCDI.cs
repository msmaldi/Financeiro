using System;
using Msmaldi.Financeiro.Website.Interfaces;

namespace Msmaldi.Financeiro.Website.BusinessLogic.CDB
{
    public class PosicaoDetalhadaCDBComCDI<TCDBComCDI>
        where TCDBComCDI : ICDI
    {
        public TCDBComCDI CDBComCDI { get; }
        public DateTime Data { get; }
        public double ValorAplicado { get; }
        public double ValorBruto { get; }
        public double ValorLiquido { get; }
        public double ValorIR { get; }
        public double ValorIOF { get; }
        public double RendimentoBruto { get; }
        public InfoPorDia[] InformacoesDiarias { get; }
    }


    
    public class InfoPorDia
    {
        public string Info { get; set; }
        public double Data { get; set; }
        public double Valor { get; set; }
    }
}