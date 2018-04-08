using Microsoft.EntityFrameworkCore;
using Msmaldi.Financeiro.Website.Data.ExternalProviders;
using Msmaldi.Financeiro.Website;
using Msmaldi.Financeiro.Website.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Msmaldi.Financeiro.Website.Data;
using static Msmaldi.Financeiro.Website.Extensions.DateTimeExtensions;

namespace Msmaldi.Financeiro.Data.Seeder
{
    public class TaxasDIOverSeeder
    {
        readonly FinanceiroDbContext _db;

        public TaxasDIOverSeeder(FinanceiroDbContext db)
        {
            _db = db;
        }

        public async Task AtualizarAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            inicio:
            if (cancellationToken.IsCancellationRequested)
                await Task.FromCanceled(cancellationToken);
            #region ObterFeriados
            List<DateTime> feriados;
            try
            {
                feriados = await _db.Feriados.Select(f => f.Data).ToListAsync(cancellationToken);
            }
            catch (Exception)
            {
                Console.WriteLine("Problemas para Obter Feriados.");
                return;
            }
            #endregion
            #region ObterTabelaDIOver
            List<DateTime> datasTaxasDIOverNoBanco;
            try
            {
                datasTaxasDIOverNoBanco = _db.TaxasDIOver.Select(di => di.Data).ToList();
            }
            catch(Exception)
            {
                Console.WriteLine("Problemas para Obter Feriados.");
                return;
            }
            #endregion

            var diasUteisNoPeriodo = DiasUteisPorPeriodo(
                inicio: DateTime.Parse("2012-08-20"),
                fim: DateTime.UtcNow,
                feriados: feriados);

            var diasParaAtualizar = diasUteisNoPeriodo.Except(datasTaxasDIOverNoBanco);
            var quantidadeDeDiasParaAtualizar = diasParaAtualizar.Count();

            Console.WriteLine($"Dias para atualizar: ({quantidadeDeDiasParaAtualizar})");
            if (quantidadeDeDiasParaAtualizar == 0)
                return;

            Console.WriteLine(String.Join(", ", diasParaAtualizar.Select(d => $"{d:dd/MM/yyyy}")));

            try
            {
                using (var cetip = new CetipMediaCDI())
                {
                    await cetip.LoginAsync();
                    var diasExistentesNaFonte = cetip.DatasDisponiveisAsync().Result;
                    var diasQueSeraoAtualizados = diasParaAtualizar.Intersect(diasExistentesNaFonte).Reverse();
                    Console.WriteLine($"Dias que serão atualizados: ({diasQueSeraoAtualizados.Count()})");
                    Console.WriteLine(String.Join(", ", diasQueSeraoAtualizados.Select(d => $"{d:dd/MM/yyyy}")));

                    foreach (var diaParaAtualizar in diasQueSeraoAtualizados)
                    {
                        if (cancellationToken.IsCancellationRequested)
                            await Task.FromCanceled(cancellationToken);
                        var diOver = 
                            await TryGetAsync(async () => await cetip.ObterPorDataAsync(diaParaAtualizar), 3);

                        Console.WriteLine($"{diOver.Data:dd/MM/yyyy}\t{diOver.Taxa}");
                        _db.TaxasDIOver.Add(diOver);
                        await _db.SaveChangesAsync(cancellationToken);
                    }
                }
            }
            catch (Exception)
            {
                goto inicio;
            }
        }

        private static async Task<DIOver> TryGetAsync(Func<Task<DIOver>> function, int times)
        {
            var i = 0;
            while (i++ < times)
            {
                try
                {
                    return await function?.Invoke();
                }
                catch (Exception)
                {
                    await Task.Delay(100);
                    Console.WriteLine($"Tentativa {i}");
                }
            }
            throw new Exception($"Falha nas {times} tentativas");
        }
    }
}
