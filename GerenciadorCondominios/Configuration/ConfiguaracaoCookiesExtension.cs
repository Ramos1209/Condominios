using Microsoft.Extensions.DependencyInjection;
using System;

namespace GerenciadorCondominios.Configuration
{
    public static class ConfiguaracaoCookiesExtension
    {
        public static void ConfigurarCookie(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "IdentityCookie";
                opt.Cookie.HttpOnly = true;
               // opt.Cookie.ExpireS = TimeSpan.FromMinutes(60);
                opt.Cookie.Path = "/Usuarios/Login";
            });
        }
    }
}
