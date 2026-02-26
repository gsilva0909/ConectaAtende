using API.ConectaAtende.Dominio.Entidades;

namespace API.ConectaAtende.Dominio.Interfaces.IRepositorios
{
    public interface IRepositorioContatos
    {
        public Task<ContatosEntidade> Adicionar(ContatosEntidade contatos);
        public Task<List<ContatosEntidade>> ObterTodos();
        public Task<ContatosEntidade> ObterPorId(Guid idContato);
        public Task<ContatosEntidade> Atualizar(Guid idContato, ContatosEntidade dto);
        public Task Excluir(Guid idContato);
        public Task<List<ContatosEntidade>> Listar(int paginaContatos, int qntdContatos);
    }
}