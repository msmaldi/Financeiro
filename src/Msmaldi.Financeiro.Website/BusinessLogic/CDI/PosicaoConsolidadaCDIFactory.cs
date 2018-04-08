using Msmaldi.Financeiro.Website.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Msmaldi.Financeiro.Website.BusinessLogic.CDI
{
    public class PosicaoConsolidadaCDIFactory
    {
        protected IQueryable<IDIOver> _taxasDIOver;

        public PosicaoConsolidadaCDIFactory(IQueryable<IDIOver> taxasDIOver)
        {
            _taxasDIOver = taxasDIOver;
        }

        protected double ValorAplicado(ICDI cdi) => cdi.PrecoUnitario * cdi.Quantidade;

        protected double Fator(ICDI cdi, DateTime naData)
        {
            var fator = 1.0;
            var taxas = _taxasDIOver
                .Where(t => t.Data >= cdi.DataDaAplicacao && t.Data < naData)
                .Select(t => t.Taxa).ToList();

            foreach (var diOver in taxas)
            {
                var fatorDiario = (Math.Pow((1.0 + diOver), (1.0 / 252.0)) - 1);
                fator *= (fatorDiario * cdi.Taxa) + 1;
            }
            return fator;
        }

        protected double ValorBruto(ICDI cdi, DateTime naData)
        {
            return Fator(cdi, naData) * cdi.PrecoUnitario * cdi.Quantidade;
        }
        
        protected double RendimentoBruto(double valorBruto, double valorAplicado) =>
            valorBruto - valorAplicado;

        protected double RendimentoBruto(ICDI cdi, DateTime naData) =>
            RendimentoBruto(ValorBruto(cdi, naData), ValorAplicado(cdi));

        protected double ValorIOF(double rendimentoBruto, int dias)
        {
            if (dias >= 30)
                return 0.0;

            var iof = rendimentoBruto * TabelaIOF[dias];
            return iof;
        }

        protected double ValorIOF(double rendimentoBruto, ICDI cdi, DateTime naData)
        {
            var dias = (int)((naData - cdi.DataDaAplicacao).TotalDays);
            return ValorIOF(rendimentoBruto, dias);
        }

        protected double ValorIOF(ICDI cdi, DateTime naData)
        {
            var rendimentoBruto = ValorBruto(cdi, naData) - ValorAplicado(cdi);
            return ValorIOF(rendimentoBruto, cdi, naData);
            
        }
        
        static Dictionary<int, double> TabelaIOF
        {
            get
            {
                var result = new Dictionary<int, double>
                {
                    { 0, 1.00 },
                    { 1, 0.96 },
                    { 2, 0.93 },
                    { 3, 0.90 },
                    { 4, 0.86 },
                    { 5, 0.83 },
                    { 6, 0.80 },
                    { 7, 0.76 },
                    { 8, 0.73 },
                    { 9, 0.70 },
                    { 10, 0.66 },
                    { 11, 0.63 },
                    { 12, 0.60 },
                    { 13, 0.56 },
                    { 14, 0.53 },
                    { 15, 0.50 },
                    { 16, 0.46 },
                    { 17, 0.43 },
                    { 18, 0.40 },
                    { 19, 0.36 },
                    { 20, 0.33 },
                    { 21, 0.30 },
                    { 22, 0.26 },
                    { 23, 0.23 },
                    { 24, 0.20 },
                    { 25, 0.16 },
                    { 26, 0.13 },
                    { 27, 0.10 },
                    { 28, 0.06 },
                    { 29, 0.03 },
                    { 30, 0.00 }
                };
                return result;
            }
        }
    }
}
