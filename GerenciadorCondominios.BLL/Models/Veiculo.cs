using System.ComponentModel.DataAnnotations;

namespace GerenciadorCondominios.BLL.Models
{
    public class Veiculo
    {

        public int VeiculoId { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatorio")]
        [StringLength(20, ErrorMessage = "Maximo de 20 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatorio")]
        [StringLength(20, ErrorMessage = "Maximo de 20 caracteres")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatorio")]
        [StringLength(20, ErrorMessage = "Maximo de 20 caracteres")]
        public string Cor { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatorio")]
        public string Placa { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
      
    }
}