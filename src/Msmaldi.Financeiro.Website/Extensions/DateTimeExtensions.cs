using System;
using System.Collections.Generic;
using System.Linq;

namespace Msmaldi.Financeiro.Website.Extensions
{
    public static class DateTimeExtensions
    {
        public static IList<DateTime>
            DiasUteisPorPeriodo(DateTime inicio, DateTime fim, IList<DateTime> feriados)
        {
            feriados = feriados ?? new List<DateTime>();
            var result = new List<DateTime>();
            while (inicio.AddHours(23) <= fim)
            {
                if (ÉDiaUtil(inicio, feriados))
                    result.Add(inicio.Date);
                inicio = inicio.AddDays(1.0);
            }
            return result;
        }

        public static DateTime
            AdicionarDiasUteis(this DateTime data, int dias, IList<DateTime> feriados)
        {
            if (dias == 0)
            {
                while (!ÉDiaUtil(data, feriados))
                    data = data.AddDays(1);
                return data;
            }
            else if (dias < 0)
            {
                while (dias != 0)
                {
                    data = data.AddDays(-1);
                    dias++;
                    while (!ÉDiaUtil(data, feriados))
                    {
                        data = data.AddDays(-1);
                    }
                }
            }
            else // if (dias > 0)
            {
                while (dias != 0)
                {
                    data = data.AddDays(1);
                    dias--;
                    while (!ÉDiaUtil(data, feriados))
                    {
                        data = data.AddDays(1);
                    }
                }
            }

            return data;
        }

        public static bool
            ÉDiaUtil(this DateTime data, IList<DateTime> feriados)
        {
            if (data.DayOfWeek == DayOfWeek.Saturday ||
                data.DayOfWeek == DayOfWeek.Sunday ||
                feriados.Any(f => f == data))
                return false;
            return true;
        }
    }
}