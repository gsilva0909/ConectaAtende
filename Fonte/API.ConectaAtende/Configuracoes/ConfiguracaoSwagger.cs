using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;

namespace API.ConectaAtende.Configuracoes
{
    [ExcludeFromCodeCoverage]
    public static class ConfiguracaoSwagger
    {
        public static IServiceCollection AdicionarConfiguracoesSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API.ConectaAtende", Version = "v1" });
            });

            return services;
        }

        public static IApplicationBuilder UsarConfiguracoesSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API.ConectaAtende v1");
            });

            return app;
        }
    }
}