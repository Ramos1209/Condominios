using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace GerenciadorCondominios.BLL.Models
{
   public class Usuario: IdentityUser<string>
    {
        public string Cpf { get; set; }
        public string Foto { get; set; }
        public bool PromeiroAcesso { get; set; }

        public StatusConta Status { get; set; }

        public virtual Collection<Apartamento> MoradoresApartamentos { get; set; }

        public virtual Collection<Apartamento> ProprietariosApartamentos { get; set; }

        public virtual Collection<Veiculo> Veiculos { get; set; }

        public virtual Collection<Servico> Servicos { get; set; }

        public virtual Collection<Evento> Eventos { get; set; }
        public virtual Collection<Pagamento> Pagamentos { get; set; }
    }

   public enum  StatusConta
   {
        Analisando,
        Aprovado,
        Reprovado
   }
}
