using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCondominios.ViewModels
{
    public class RegistroViewModel
    {

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(40,  ErrorMessage = "Use menos Caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Telefone { get; set; }

      //  [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Foto { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "Use menos Caracteres")]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(10, ErrorMessage = "Use menos Caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme sua senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(10, ErrorMessage = "Use menos Caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme sua senha")]
        [Compare("Senha",ErrorMessage = "As senhas não conferem")]
        public string ConfirmaSenha { get; set; }

    }
}
