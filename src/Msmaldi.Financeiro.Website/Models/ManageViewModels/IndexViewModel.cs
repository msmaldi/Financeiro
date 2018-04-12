using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Msmaldi.Financeiro.Website.Models.ManageViewModels
{
    public class IndexViewModel
    {
        
        [Display(Name = "Usuário")]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {0} não é um endereço de e-mail válido.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "O campo {0} não é um número de telefone válido.")]
        [Display(Name = "Celular")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
