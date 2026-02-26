using API.ConectaAtende.Aplicacao.Dtos;
using API.ConectaAtende.Dominio.Entidades;

namespace API.ConectaAtende.Aplicacao.Interfaces
{
    public interface IServicoContatos
    {
        public Task<ContatosEntidade> CriaContato(ContatosDto dto);
        public Task<List<ContatosEntidade>> ObterTodos();
        public Task<ContatosEntidade> ObterPorId(Guid idContato);
        public Task<ContatosEntidade> Atualizar(Guid idContato, ContatosDto dto);
        public Task Excluir(Guid idContato);
        public Task<List<ContatosEntidade>> Listar(int paginaContatos, int qntdContatos);
    }
}