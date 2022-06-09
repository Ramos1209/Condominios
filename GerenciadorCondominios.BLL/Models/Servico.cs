using System.Collections.Generic;

namespace GerenciadorCondominios.BLL.Models
{
    public class Servico
    {
        public int ServicoId { get; set; }

        public string Nome { get; set; }

        public decimal Valor { get; set; }

        public StatuServico Status { get; set; }

        public string UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<ServicoPredio>ServicoPredios { get; set; }
    }

    public enum StatuServico
    {
        Aceito, Resusado, Pendente
    }
}