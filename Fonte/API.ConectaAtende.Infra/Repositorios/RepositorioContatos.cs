using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ConectaAtende.Dominio.Entidades;
using API.ConectaAtende.Dominio.Interfaces.IRepositorios;
using API.ConectaAtende.Infra.Contexto;

namespace API.ConectaAtende.Infra.Repositorios
{
    public class RepositorioContatos : IRepositorioContatos
    {
        private readonly DbContextContatos _contexto;

        public RepositorioContatos(DbContextContatos contexto)
        {
            _contexto = contexto;
        }

        public Task<ContatosEntidade> Adicionar(ContatosEntidade contatos)
        {
            if (_contexto.Contatos == null)
                return Task.FromResult<ContatosEntidade>(null);

            _contexto.Contatos.Add(contatos);
            _contexto.Salvar();
            return Task.FromResult(contatos);
        }

        public Task<List<ContatosEntidade>> ObterTodos()
        {
            if (_contexto.Contatos == null)
                return Task.FromResult<List<ContatosEntidade>>(null);

            return Task.FromResult(_contexto.Contatos.ToList());
        }

        public Task<ContatosEntidade> ObterPorId(Guid idContato)
        {
            if (_contexto.Contatos == null)
                return Task.FromResult<ContatosEntidade>(null);

            return Task.FromResult(_contexto.Contatos.FirstOrDefault(c => c.Id == idContato));
        }

        public Task<ContatosEntidade> Atualizar(Guid idContato, ContatosEntidade dto)
        {
            if (_contexto.Contatos == null)
                return Task.FromResult<ContatosEntidade>(null);

            var existente = _contexto.Contatos.FirstOrDefault(c => c.Id == idContato);
            if (existente == null)
                return Task.FromResult<ContatosEntidade>(null);

            existente.Nome = dto.Nome;
            existente.Email = dto.Email;
            existente.Telefone = dto.Telefone;
            _contexto.Salvar();

            return Task.FromResult(existente);
        }

        public Task Excluir(Guid idContato)
        {
            if (_contexto.Contatos != null)
                _contexto.Excluir(idContato);

            return Task.CompletedTask;
        }

        public Task<List<ContatosEntidade>> Listar(int paginaContatos, int qntdContatos)
        {
            if (_contexto.Contatos == null)
                return Task.FromResult<List<ContatosEntidade>>(null);

            var skip = (paginaContatos - 1) * qntdContatos;
            var itens = _contexto.Contatos
                                 .Skip(skip)
                                 .Take(qntdContatos)
                                 .ToList();
            return Task.FromResult(itens);
        }
    }
}