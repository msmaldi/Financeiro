using System.ComponentModel.DataAnnotations;

namespace Msmaldi.Financeiro.Website.Models.SwingTradeViewModels
{
    public class NewStock
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Código")]
        public string Symbol { get; set; }
    }
}