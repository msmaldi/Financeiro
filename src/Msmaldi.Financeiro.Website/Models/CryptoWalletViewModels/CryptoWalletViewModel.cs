using System.ComponentModel.DataAnnotations;

namespace Msmaldi.Financeiro.Website.Models.CryptoWalletViewModels
{
    public class CryptoWalletViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Rótulo")]
        public string Label { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Quantidade")]
        [Range(0.00000001, 999999999.0)]
        public double Quantidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Valor de Aquisição")]
        [Range(0.01, 999999999.0)]
        public double ValorDeAquisicao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Criptomoeda")]
        public string CriptoCurrencyId { get; set; }
    }
}