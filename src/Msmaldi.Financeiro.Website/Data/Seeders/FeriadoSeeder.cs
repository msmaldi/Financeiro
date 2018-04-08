using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Msmaldi.Financeiro.Website.Entities;

namespace Msmaldi.Financeiro.Website.Data.Seeders
{
    public class FeriadoSeeder
    {        
        private readonly FinanceiroDbContext _db;
        
        public FeriadoSeeder(FinanceiroDbContext db)
        {
            _db = db;
        }

        public async Task AtualizarAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var feriadosNoBanco = await _db.Feriados.ToListAsync(cancellationToken);

            var lines = File.ReadAllLines(@"./Data/Seeds/Feriados.txt");
            var feriados = lines.Select(l => ObterFeriado(l));
            
            var feriadosParaAtualizar = feriados.Except(feriadosNoBanco, FeriadoComparer.Instance);
            
            await _db.Feriados.AddRangeAsync(feriadosParaAtualizar, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        private static Feriado ObterFeriado(string conteudo)
        {
            var split = conteudo.Split('\t');
            var data = DateTime.ParseExact(split[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var nome = split[2];
            return new Feriado(data, nome);
        }
    }

    class FeriadoComparer : IEqualityComparer<Feriado>
    {
        public bool Equals(Feriado x, Feriado y) => (x.Data == y.Data);
        public int GetHashCode(Feriado obj) => obj.Data.GetHashCode();

        public static FeriadoComparer Instance => new FeriadoComparer();
    }
}