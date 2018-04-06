using System;
using System.Collections.Generic;
using System.Linq;

namespace Msmaldi.Financeiro.Website.Entities
{
    public class CDBComCDI
    {
        public int Id { get; protected set; }

        public DateTime DataDaAplicacao { get; protected set; }
        public DateTime DataDoVencimento { get; protected set; }
        public double PrecoUnitario { get; protected set; }
        public int Quantidade { get; protected set; }
        public double Taxa { get; protected set; }
        
        public HashSet<ResgateCDBComCDI> Resgates => _resgates;
        public int QuantidadeAtual => Quantidade - Resgates.Sum(c => c.Quantidade);

        public Guid UserId { get; set; }
        public User User { get; set; }

        public CDBComCDI(DateTime dataDaAplicacao,
                         DateTime dataDoVencimento,
                           double precoUnitario,
                              int quantidade,
                           double taxa,
                             User user)
            : this (dataDaAplicacao, dataDoVencimento, precoUnitario, quantidade, taxa, user.Id)
        {
        }

        public CDBComCDI(DateTime dataDaAplicacao,
                         DateTime dataDoVencimento,
                            double precoUnitario,
                               int quantidade,
                            double taxa,
                              Guid userId)
            : this ()
        {
            DataDaAplicacao = dataDaAplicacao;
            DataDoVencimento = dataDoVencimento;
            PrecoUnitario = precoUnitario;
            Quantidade = quantidade;
            Taxa = taxa;
            UserId = userId;
        }

        public CDBComCDIResult PodeAdicionarResgate(ResgateCDBComCDI resgate)
        {
            if (resgate.Quantidade <= 0)
                return CDBComCDIResult.Failed(new Tuple<string, string>("Quantidade", "A quantidade não pode ser menor que zero."));
            if (resgate.Data <= DataDaAplicacao)
                return CDBComCDIResult.Failed(new Tuple<string, string>("Data", "A Data do Resgate não pode ser menor que a data da aplicação."));
            if (resgate.Data >= DataDoVencimento)
                return CDBComCDIResult.Failed(new Tuple<string, string>("Data", "A Data do Resgate não pode ser maior que a data do vencimento."));
            if (Resgates.Any(c => c.Data == resgate.Data))
                return CDBComCDIResult.Failed(new Tuple<string, string>("Data", "Já existe um resgate nesta data."));

            var quantidadeAtualizada = QuantidadeAtual - resgate.Quantidade;
            if (quantidadeAtualizada < 0)
                return CDBComCDIResult.Failed(new Tuple<string, string>("Quantidade", "A soma final do saldo não pode ser menor que zero.")); 
                
            return CDBComCDIResult.Success;
        }

        public void AdicionarResgate(ResgateCDBComCDI resgate)
        {
            _resgates.Add(resgate);
        }

        public CDBComCDI()
        {
			_resgates = new HashSet<ResgateCDBComCDI>();
        }

		private HashSet<ResgateCDBComCDI> _resgates;
    }

    public class CDBComCDIResult
    {

        public static CDBComCDIResult Success => new CDBComCDIResult();

        public static CDBComCDIResult Failed(params Tuple<string, string>[] errors)
        {
            return new CDBComCDIResult(errors);
        }


        public bool Succeeded => _errors.Count == 0;
        private List<Tuple<string, string>> _errors = new List<Tuple<string, string>>();
        public List<Tuple<string, string>> Errors => _errors;

        private CDBComCDIResult()
        {
        }

        private CDBComCDIResult(params Tuple<string, string>[] errors)
        {
            _errors.AddRange(errors);
        }
    }
}