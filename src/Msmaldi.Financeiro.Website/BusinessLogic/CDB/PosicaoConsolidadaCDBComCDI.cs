using Msmaldi.Financeiro.Website.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Msmaldi.Financeiro.Website.BusinessLogic.CDB
{
    public class PosicaoConsolidadaCDBComCDI<TCDBComCDI>
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

        internal PosicaoConsolidadaCDBComCDI(TCDBComCDI cdbComCDI,
            DateTime data, double valorBruto, double valorLiquido,
            double valorIR, double valorIOF)
        {
            CDBComCDI = cdbComCDI;
            ValorAplicado = CDBComCDI.PrecoUnitario * CDBComCDI.Quantidade;
            Data = data;
            ValorBruto = valorBruto;
            ValorLiquido = valorLiquido;
            ValorIR = valorIR;
            ValorIOF = valorIOF;
            RendimentoBruto = ValorBruto - ValorAplicado;
        }
    }
}
