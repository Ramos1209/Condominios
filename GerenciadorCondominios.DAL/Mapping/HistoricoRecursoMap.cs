using GerenciadorCondominios.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorCondominios.DAL.Mapping
{
    public class HistoricoRecursoMap : IEntityTypeConfiguration<HistoricoRecurso>
    {
        public void Configure(EntityTypeBuilder<HistoricoRecurso> builder)
        {
            builder.HasKey(x => x.HistoricoRecursoId);
            builder.Property(x => x.Ano).IsRequired();
            builder.Property(x => x.Tipo).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Dia).IsRequired();
            builder.Property(x => x.MesId).IsRequired();


            builder.HasOne(x => x.Mes).WithMany(x => x.HistoricoRecursos).HasForeignKey(x => x.MesId);
            builder.ToTable("HistoricoRecursos");   
        }
    }
}
