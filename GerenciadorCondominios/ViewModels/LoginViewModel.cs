using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominios.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "O Campo {0} é obrigátorio")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigátorio")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
