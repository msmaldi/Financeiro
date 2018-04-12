using Msmaldi.Financeiro.Website.BusinessLogic.CDB;
using Msmaldi.Financeiro.Website.BusinessLogic.CDI;
using Msmaldi.Financeiro.Website.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Msmaldi.Financeiro.Website.BusinessLogic.CDB
{
    public class PosicaoConsolidadaCDBComCDIFactory : PosicaoConsolidadaCDIFactory
    {
        public PosicaoConsolidadaCDBComCDIFactory(IQueryable<IDIOver> taxasDIOver)
            : base(taxasDIOver)
        {
        }

        public PosicaoConsolidadaCDBComCDI<TCDBComCDI> ObterPosicaoConsolidada<TCDBComCDI>(
            TCDBComCDI cdbComCDI) where TCDBComCDI : ICDBComCDI
        {
            return ObterPosicaoConsolidada(cdbComCDI, DateTime.Today);
        }

        public PosicaoConsolidadaCDBComCDI<TCDBComCDI> ObterPosicaoConsolidada<TCDBComCDI>(
            TCDBComCDI cdbComCDI, DateTime naData) where TCDBComCDI : ICDBComCDI
        {
            if (naData > cdbComCDI.DataDoVencimento)
            naData = cdbComCDI.DataDoVencimento;
            return new PosicaoConsolidadaCDBComCDI<TCDBComCDI>(cdbComCDI, naData,
                valorBruto: ValorBruto(cdbComCDI, naData),
                valorIOF: ValorIOF(cdbComCDI, naData),
                valorIR: ValorIR(cdbComCDI, naData),
                valorLiquido: ValorLiquido(cdbComCDI, naData));
        }

        public double ValorIR(ICDBComCDI cdbComCDI, DateTime naData)
        {
            var dias = (int)((naData - cdbComCDI.DataDaAplicacao).TotalDays);

            var rendimento = ValorBruto(cdbComCDI, naData) - ValorAplicado(cdbComCDI) - ValorIOF(cdbComCDI, naData);
            var ir = rendimento * TabelaIR(dias);
            return ir;
        }

        public double ValorLiquido(ICDBComCDI cdbComCDI, DateTime naData)
        {
            return ValorBruto(cdbComCDI, naData) - ValorIOF(cdbComCDI, naData) - ValorIR(cdbComCDI, naData);
        }

        static double TabelaIR(int dias)
        {
            return dias <= 180 ? 0.225 :
                   dias <= 360 ? 0.200 :
                   dias <= 720 ? 0.175 :
                                 0.150;
        }
    }
}
