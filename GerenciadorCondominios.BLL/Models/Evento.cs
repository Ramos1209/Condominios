using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominios.BLL.Models
{
    public class Evento
    {
        public int EventoId { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "Maximo de 20 caracteres")]
        public  string Nome { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public DateTime DataEvento { get; set; }

        public string UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
