using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorCondominios.Configuration
{
    public static class ConfigurationIdentityExtension
    {
        public static void ConfigurarNomeUsuario(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(opt =>
            {
                opt.User.AllowedUserNameCharacters = "abcdefghijlmnopqtrsuvxzwykABCDEFGHIJLMNOPQTRSUVXZWYK_.@*#";
                opt.User.RequireUniqueEmail = true;
            });
        }

        public static void ConfigurarSenhaUsuario(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequiredUniqueChars = 0;
            });
        }
    }
}
