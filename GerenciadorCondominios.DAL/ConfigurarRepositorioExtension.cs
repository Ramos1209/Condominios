using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using GerenciadorCondominios.DAL.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorCondominios.DAL
{
    public static class ConfigurarRepositorioExtension
    {
        public static void ConfigurarRepositorio(this IServiceCollection services)
        {

            services.AddIdentity<Usuario, Funcao>().AddEntityFrameworkStores<CondominioContext>();

            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IFuncaoRepository, FuncaoRepository>();
            services.AddTransient<IVeiculoRepository, VeiculoRepository>();
            services.AddTransient<IEventoRepository, EventoRepository>();
            services.AddTransient<IServicoRepository, ServicoRepository>();
            services.AddTransient<IServicoPredioRepository, ServicoPredioRepository>();
            services.AddTransient<IHistoricoRecursoRepository, HistoricoRecursoRepository>();
            services.AddTransient<IApartamentoRepository, ApartamentoRepository>();
            services.AddTransient<IAluguelRepository, AluguelRepository>();
            services.AddTransient<IMesRepository, MesRepository>();
            services.AddTransient<IPagamentoRepository, PagamentoRepository>();
        }
    }
}
