using Microsoft.Extensions.DependencyInjection;
using System;

namespace GerenciadorCondominios.Configuration
{
    public static class ConfiguaracaoCookiesExtension
    {
        public static void ConfigurarCookie(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(opcoes =>
            {
                opcoes.Cookie.Name = "IdentityCookie";
                opcoes.Cookie.HttpOnly = true;
                opcoes.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                opcoes.LoginPath = "/Usuarios/Login";
                opcoes.AccessDeniedPath = "/Usuarios/AcessoNegado";
            });
        }
    }
}
