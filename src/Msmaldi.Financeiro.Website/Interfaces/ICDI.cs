using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Msmaldi.Financeiro.Website.Interfaces
{
    public interface ICDI
    {
        DateTime DataDaAplicacao { get; }
        DateTime DataDoVencimento { get; }
        double PrecoUnitario { get; }
        int Quantidade { get; }
        double Taxa { get; }
        IEnumerable<IResgateCDI> Resgates { get; }
    }
}
