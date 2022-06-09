using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorCondominios.BLL.Models
{
    public class Aluguel
    {
        public int AluguelId { get; set; }


        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [Range(0,int.MaxValue, ErrorMessage = "O valor  inválido")]
        public decimal Valor { get; set; }

        public int MesId { get; set; }

        [DisplayName("Mês")]
        public Mes Mes { get; set; }


        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [Range(2020,2030, ErrorMessage = "O valor  inválido")]
        public int Ano { get; set; }


        public virtual ICollection<Pagamento> Pagamentos { get; set; }
    }
}