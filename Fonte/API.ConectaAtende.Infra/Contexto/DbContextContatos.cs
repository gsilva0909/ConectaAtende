using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using API.ConectaAtende.Dominio.Entidades;

namespace API.ConectaAtende.Infra.Contexto
{
    /// <summary>
    /// “Mini‑banco” em ficheiro.
    /// carrega a lista de contatos do disco e a mantém em memória
    /// enquanto a aplicação estiver viva. sempre que algo for alterado,
    /// chama‑se <see cref="Salvar"/> para persistir o ficheiro.
    /// </summary>
    public class DbContextContatos
    {
        private const string Arquivo = "Dados/contatos.json";
        private readonly object _lock = new();

        public List<ContatosEntidade> Contatos { get; private set; }

        public DbContextContatos()
        {
            Carregar();
        }

        private void Carregar()
        {
            if (File.Exists(Arquivo))
            {
                var json = File.ReadAllText(Arquivo);
                Contatos = JsonSerializer
                           .Deserialize<List<ContatosEntidade>>(json)
                           ?? new List<ContatosEntidade>();
            }
            else
            {
                Contatos = new List<ContatosEntidade>();
            }
        }

        public void Salvar()
        {
            lock (_lock)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Arquivo)!);
                var json = JsonSerializer.Serialize(Contatos,
                    new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(Arquivo, json);
            }
        }

        public void Excluir(Guid id)
        {
            var contato = Contatos.FirstOrDefault(c => c.Id == id);
            if (contato != null)
            {
                Contatos.Remove(contato);
                Salvar();
            }
        }
    }
}