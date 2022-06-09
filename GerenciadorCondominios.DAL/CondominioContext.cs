using System;
using System.Reflection;
using GerenciadorCondominios.BLL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCondominios.DAL
{
    public class CondominioContext: IdentityDbContext<Usuario, Funcao, string>
    {
        public CondominioContext(DbContextOptions<CondominioContext> opcoes): base(opcoes)
        {
                
        }
        public DbSet<Aluguel> Alugueis { get; set; }
        public DbSet<Apartamento> Apartamentos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Funcao> Funcoes { get; set; }
        public DbSet<HistoricoRecurso> HistoricoRecursos { get; set; }
        public DbSet<Mes> Meses { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }

        public DbSet<ServicoPredio> ServicoPredios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
