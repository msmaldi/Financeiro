using System;
using System.ComponentModel.DataAnnotations;
using Msmaldi.Financeiro.Website.DataAnnotations;

namespace Msmaldi.Financeiro.Website.Models.CDBComCDIViewModels
{
    public class CDBComCDIViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Data da Aplicação")]
        [RangeDate]
        [LessThan(nameof(DataDoVencimento), ErrorMessage = "O campo {0} deve ser menor que a Data do Vencimento.")]
        public DateTime DataDaAplicacao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Data do Vencimento")]
        [GreaterThan(nameof(DataDaAplicacao), ErrorMessage = "O campo {0} deve ser maior que a Data da Aplicação.")]
        public DateTime DataDoVencimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Preço Unitario")]
        [Range(0.01, 100000000.0)]
        public double PrecoUnitario { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Quantidade")]
        [RangeNumber(1, 100000000.0)]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Taxa")]
        [RangeNumber(50.0, 150.0)]
        public double Taxa { get; set; }
    }

    public class RangeNumber : RangeAttribute
    {
        public RangeNumber(double minimum, double maximum) : base(minimum, maximum)
        {
        }
    }

    public class RangeDateAttribute : RangeAttribute
    {
        public RangeDateAttribute()
            : base(typeof(DateTime),
        DateTime.Now.AddYears(-10).ToShortDateString(), DateTime.Now.AddYears(10).ToShortDateString()) { }
    }
}