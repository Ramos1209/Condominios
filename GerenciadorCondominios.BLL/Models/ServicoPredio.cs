using System;

namespace GerenciadorCondominios.BLL.Models
{
    public class ServicoPredio
    {
        public int  ServicoPredioId { get; set; }

        public int ServicoId { get; set; }


        public  Servico Servico { get; set; }

        public DateTime DataExecusao { get; set; }
    }
}