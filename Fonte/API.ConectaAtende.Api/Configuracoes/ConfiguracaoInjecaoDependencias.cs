using API.ConectaAtende.Aplicacao.Interfaces;
using API.ConectaAtende.Aplicacao.Servicos;
using API.ConectaAtende.Dominio.Interfaces.IRepositorios;
using API.ConectaAtende.Infra.Contexto;
using API.ConectaAtende.Infra.Repositorios;

namespace API.ConectaAtende.Api.Configuracoes
{
    public static class ConfiguracaoInjecaoDependencias
    {
        public static void InjecaoDependencias(IServiceCollection services)
        {
            #region Contatos
            services.AddSingleton<DbContextContatos>();
            services.AddScoped<IRepositorioContatos, RepositorioContatos>();
            services.AddScoped<IServicoContatos, ServicoContatos>();
            #endregion

            #region Tickets
            //services.AddSingleton<DbContextTickets>();
            //services.AddScoped<IRepositorioTickets, RepositorioTickets>();
            //services.AddScoped<IServicoTickets, ServicoTickets>();
            #endregion

            #region Triagem
            //services.AddSingleton<DbContextTriagem>();
            //services.AddScoped<IRepositorioTriagem, RepositorioTriagem>();
            //services.AddScoped<IServicoTriagem, ServicoTriagem>();
            #endregion
        }
    }
}