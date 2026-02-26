using API.ConectaAtende.Aplicacao.Dtos;
using API.ConectaAtende.Aplicacao .Interfaces;
using API.ConectaAtende.Dominio.Entidades;
using API.ConectaAtende.Dominio.Interfaces.IRepositorios;

namespace API.ConectaAtende.Aplicacao.Servicos
{
    public class ServicoContatos : IServicoContatos
    {
        private readonly IRepositorioContatos _repositorioContatos;
        public ServicoContatos(IRepositorioContatos repositorioContatos)
        {
            _repositorioContatos = repositorioContatos;
        }

        public Task<ContatosEntidade> CriaContato(ContatosDto contatos)
        {
            if (contatos == null)
                return null;

            var telefone = LimparTelefone(contatos.Telefone);
            if (telefone.Length == 0)
                return Task.FromResult<ContatosEntidade>(null);

            var contatosEntidade = new ContatosEntidade
            {
                Id = Guid.NewGuid(),
                Nome = contatos.Nome,
                Email = contatos.Email,
                Telefone = contatos.Telefone
            };

            var adicionaContatos = _repositorioContatos.Adicionar(contatosEntidade);
            if (adicionaContatos == null)
                return null;

            return adicionaContatos;
        }
        
        public Task<List<ContatosEntidade>> ObterTodos()
        {
            var contatos = _repositorioContatos.ObterTodos();
            if (contatos == null)
                return null;

            return contatos;
        }

        public Task<ContatosEntidade> ObterPorId(Guid idContato)
        {
            if (idContato == Guid.Empty)
                return null;

            var contato = _repositorioContatos.ObterPorId(idContato);
            if (contato == null)
                return null;

            return contato;
        }

        public Task<ContatosEntidade> Atualizar(Guid idContato, ContatosDto contato)
        {
            if (idContato == Guid.Empty || contato == null)
                return null;

            var contatoEntidade = new ContatosEntidade
            {
                Id = idContato,
                Nome = contato.Nome,
                Email = contato.Email,
                Telefone = contato.Telefone
            };

            var atualizaContato = _repositorioContatos.Atualizar(idContato, contatoEntidade);
            if (atualizaContato == null)
                return null;

            return atualizaContato;
        }

        public Task Excluir(Guid idContato)
        {
            if (idContato == Guid.Empty)
                return null;

            var excluirContato = _repositorioContatos.Excluir(idContato);
            if (excluirContato == null)
                return null;

            return excluirContato;
        }

        public Task<List<ContatosEntidade>> Listar(int paginaContatos, int qntdContatos)
        {
            if (paginaContatos <= 0 || qntdContatos <= 0)
                return null;

            var contatos = _repositorioContatos.Listar(paginaContatos, qntdContatos);
            if (contatos == null)
                return null;

            return contatos;
        }

        private static string LimparTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return string.Empty;

            return new string(telefone.Where(char.IsDigit).ToArray());
        }
    }
}