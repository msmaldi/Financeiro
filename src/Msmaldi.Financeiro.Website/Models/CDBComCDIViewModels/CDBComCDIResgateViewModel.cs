using System;
using System.ComponentModel.DataAnnotations;

namespace Msmaldi.Financeiro.Website.Models.CDBComCDIViewModels
{
    public class CDBComCDIResgateViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Data do Resgate")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }
    }
}