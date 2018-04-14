using System.ComponentModel.DataAnnotations;

namespace Msmaldi.Financeiro.Website.Models.SwingTradeViewModels
{
    public class SwingTradeViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Código")]
        public string Symbol { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Valor de Aquisição")]
        [Range(0.01, 100000.0)]
        public double ValorDeAquisicao { get; set; }
    }
}