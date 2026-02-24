# API.ConectaAtende.Infra

Projeto de **Camada de Infraestrutura** da arquitetura da API ConectaAtende.

## 📋 Descrição

A camada de Infraestrutura é responsável por **implementar os detalhes técnicos e de persistência de dados**. Este projeto contém a configuração do banco de dados, implementação dos repositórios, integração com APIs externas e outras dependências tecnológicas.

## 🎯 Responsabilidades

- ✅ **Contexto de Banco de Dados**: Configuração do Entity Framework Core (DbContext)
- ✅ **Implementação de Repositórios**: Implementar as interfaces definidas no Domínio
- ✅ **Mapeamento de Entidades**: Configuração de como as entidades são persistidas no BD (Fluent API ou Data Annotations)
- ✅ **Migrações de Banco**: Gerenciar versões do schema do banco de dados
- ✅ **Integrações Externas**: Chamadas a APIs, serviços cloud, etc
- ✅ **Unit of Work**: Gerenciar transações e trabalho coordenado com múltiplos repositórios

## 📁 Estrutura Esperada

```
API.ConectaAtende.Infra/
├── Data/
│   ├── ConectaAtendeContext.cs           # DbContext principal
│   ├── Mappings/                         # Configurações de mapeamento
│   │   ├── TicketMapping.cs              # Mapeamento da entidade Ticket
│   │   ├── UsuarioMapping.cs             # Mapeamento da entidade Usuario
│   │   └── ...
├── Repositories/
│   ├── TicketRepository.cs               # Implementação de ITicketRepository
│   ├── UsuarioRepository.cs              # Implementação de IUsuarioRepository
│   └── ...
└── README.md                             # Este arquivo
```

## 💡 Exemplos de Implementação

### DbContext Configurado
```csharp
namespace API.ConectaAtende.Infra.Data
{
    public class ConectaAtendeContext : DbContext
    {
        public ConectaAtendeContext(DbContextOptions<ConectaAtendeContext> options) 
            : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Aplicar configurações de mapeamento
            modelBuilder.ApplyConfiguration(new TicketMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
        }
    }
}
```

### Mapeamento de Entidade
```csharp
namespace API.ConectaAtende.Infra.Data.Mappings
{
    public class TicketMapping : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(255);
                
            builder.Property(t => t.Descricao)
                .HasMaxLength(5000);
                
            builder.Property(t => t.DataCriacao)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
```

### Repositório Implementado
```csharp
namespace API.ConectaAtende.Infra.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ConectaAtendeContext _context;
        
        public TicketRepository(ConectaAtendeContext context)
        {
            _context = context;
        }
        
        public async Task<Ticket> ObterPorIdAsync(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }
        
        public async Task<IEnumerable<Ticket>> ObterTodosAsync()
        {
            return await _context.Tickets.ToListAsync();
        }
        
        public async Task CriarAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
        }
        
        public async Task AtualizarAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeletarAsync(int id)
        {
            var ticket = await ObterPorIdAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}
```

## ⚙️ Dependencies

Este projeto deve referenciar:
- 📦 `Microsoft.EntityFrameworkCore`
- 📦 `Microsoft.EntityFrameworkCore.SqlServer` (ou outro provedor)
- 📦 **Domínio**: Para implementar interfaces e trabalhar com entidades

Pode referenciar bibliotecas externas necessárias (ex: bibliotecas de integração com APIs externas).

## 🔒 Regras de Ouro

1. **Nunca quebre o contrato do Domínio**: Implemente exatamente as interfaces definidas no Domínio
2. **Referencie apenas Domínio**: Não importe de Serviço ou Controllers
3. **Detalhes técnicos aqui**: Tudo relacionado a banco de dados vai aqui
4. **Sem lógica de negócio**: Deixe validações para o Domínio
5. **Async/Await**: Use operações assíncronas para banco de dados

## 📚 Configuração Padrão

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ConectaAtende;Trusted_Connection=true;"
  }
}
```

### Startup.cs (registrar no DI)
```csharp
services.AddDbContext<ConectaAtendeContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
);

services.AddScoped<ITicketRepository, TicketRepository>();
services.AddScoped<IUsuarioRepository, UsuarioRepository>();
```

## 📚 Referências

- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Repository Pattern](https://martinfowler.com/eaaCatalog/repository.html)
- [Unit of Work Pattern](https://martinfowler.com/eaaCatalog/unitOfWork.html)

---

**Resumo**: Este projeto é o "garagista" da aplicação. Implementa COMO os dados são armazenados e acessados.
