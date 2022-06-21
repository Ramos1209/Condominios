using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominios.ViewModels
{
    public class AtualizaUsuarioViewModel
    {
        public string UsuarioId { get; set; }

        [Required(ErrorMessage = " O campo é {0} obrigatorio")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        public string Nome { get; set; }

       
        [Required(ErrorMessage = " O campo é {0} obrigatorio")]
        public string Cpf { get; set; }
     
        [Required(ErrorMessage = " O campo é {0} obrigatorio")]
        public string Telefone { get; set; }
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        [Required(ErrorMessage = " O campo é {0} obrigatorio")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        public string Foto { get; set; }
    }
}
