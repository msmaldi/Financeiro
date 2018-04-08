using System;

namespace Msmaldi.Financeiro.Website.Interfaces
{
    public interface IResgateCDI
    {
        DateTime Data { get; set; }
        int Quantidade { get; set; }
    }
}