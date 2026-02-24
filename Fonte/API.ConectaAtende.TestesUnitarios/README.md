# API.ConectaAtende.TestesUnitarios

Projeto de **Testes Unitários** da arquitetura da API ConectaAtende.

## 📋 Descrição

O projeto de Testes Unitários é responsável por **validar o funcionamento correto do código**, garantindo que cada unidade (método/classe) funciona conforme esperado. Os testes garantem qualidade, facilitam refatorações e documentam o comportamento esperado da aplicação.

## 🎯 Responsabilidades

- ✅ **Testes de Domínio**: Validar regras de negócio e comportamento de entidades
- ✅ **Testes de Serviços**: Testar a orquestração e lógica de aplicação
- ✅ **Testes de Repositórios**: Validar operações de persistência (com mocks ou banco teste)
- ✅ **Mocks e Stubs**: Simular dependências externas
- ✅ **Cobertura de Testes**: Garantir que o código crítico está testado
- ✅ **Testes de Validação**: Validadores e regras de negócio

## 📁 Estrutura Esperada

```
API.ConectaAtende.TestesUnitarios/
├── Domain/
│   ├── Entities/
│   │   ├── TicketTests.cs              # Testes da entidade Ticket
│   │   ├── UsuarioTests.cs             # Testes da entidade Usuario
│   │   └── ...
│   └── ...
├── Service/
│   ├── TicketServiceTests.cs           # Testes do serviço de Ticket
│   ├── UsuarioServiceTests.cs          # Testes do serviço de Usuario
│   └── ...
├── Repository/
│   ├── TicketRepositoryTests.cs        # Testes do repositório (com mock)
│   ├── UsuarioRepositoryTests.cs
│   └── ...
├── Validators/
│   ├── CriarTicketValidatorTests.cs    # Testes de validadores
│   └── ...
├── Fixtures/
│   ├── TicketFixture.cs                # Dados de teste reutilizáveis
│   ├── UsuarioFixture.cs
│   └── ...
├── Mocks/
│   ├── MockTicketRepository.cs         # Mocks de repositórios
│   └── ...
└── README.md                           # Este arquivo
```

## 💡 Exemplos de Implementação

### Teste Simples de Entidade
```csharp
namespace API.ConectaAtende.TestesUnitarios.Domain.Entities
{
    public class TicketTests
    {
        [Fact]
        public void CriarTicket_ComDadosValidos_DeveCriarComSucesso()
        {
            // Arrange
            var titulo = "Ticket Teste";
            var descricao = "Descrição do teste";
            var usuarioId = 1;

            // Act
            var ticket = new Ticket(titulo, descricao, usuarioId);

            // Assert
            Assert.NotNull(ticket);
            Assert.Equal(titulo, ticket.Titulo);
            Assert.Equal(descricao, ticket.Descricao);
            Assert.Equal(usuarioId, ticket.UsuarioId);
            Assert.Equal(Status.Aberto, ticket.Status);
        }

        [Fact]
        public void CriarTicket_SemTitulo_DeveLancarExcecao()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => 
                new Ticket(string.Empty, "Descrição", 1));
        }
    }
}
```

### Teste de Serviço com Mocks
```csharp
namespace API.ConectaAtende.TestesUnitarios.Service
{
    public class TicketServiceTests
    {
        private readonly Mock<ITicketRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TicketService _service;

        public TicketServiceTests()
        {
            _repositoryMock = new Mock<ITicketRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new TicketService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ObterPorIdAsync_ComIdValido_DeveRetornarTicket()
        {
            // Arrange
            var ticketId = 1;
            var ticket = new Ticket("Titulo", "Descricao", 1) { Id = ticketId };
            var ticketResponse = new TicketResponse { Id = ticketId, Titulo = "Titulo" };

            _repositoryMock
                .Setup(r => r.ObterPorIdAsync(ticketId))
                .ReturnsAsync(ticket);

            _mapperMock
                .Setup(m => m.Map<TicketResponse>(ticket))
                .Returns(ticketResponse);

            // Act
            var resultado = await _service.ObterPorIdAsync(ticketId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(ticketId, resultado.Id);
            _repositoryMock.Verify(r => r.ObterPorIdAsync(ticketId), Times.Once);
        }

        [Fact]
        public async Task ObterPorIdAsync_ComIdInvalido_DeveThrow()
        {
            // Arrange
            var ticketId = 999;
            _repositoryMock
                .Setup(r => r.ObterPorIdAsync(ticketId))
                .ReturnsAsync((Ticket)null);

            // Act & Assert
            await Assert.ThrowsAsync<TicketNaoEncontradoException>(
                () => _service.ObterPorIdAsync(ticketId));
        }
    }
}
```

### Fixture para Dados de Teste
```csharp
namespace API.ConectaAtende.TestesUnitarios.Fixtures
{
    public class TicketFixture
    {
        public Ticket CriarTicketValido(int id = 1)
        {
            return new Ticket("Ticket Teste", "Descrição teste", 1) { Id = id };
        }

        public IEnumerable<Ticket> CriarTicketsValidos(int quantidade = 5)
        {
            var tickets = new List<Ticket>();
            for (int i = 1; i <= quantidade; i++)
            {
                tickets.Add(new Ticket($"Ticket {i}", "Descrição", 1) { Id = i });
            }
            return tickets;
        }

        public CriarTicketRequest CriarCriarTicketRequest()
        {
            return new CriarTicketRequest
            {
                Titulo = "Novo Ticket",
                Descricao = "Descrição do novo ticket",
                UsuarioId = 1
            };
        }
    }
}
```

### Teste de Validador
```csharp
namespace API.ConectaAtende.TestesUnitarios.Validators
{
    public class CriarTicketValidatorTests
    {
        private readonly CriarTicketValidator _validator;

        public CriarTicketValidatorTests()
        {
            _validator = new CriarTicketValidator();
        }

        [Fact]
        public async Task Validar_ComDadosValidos_DeveRetornarSemErros()
        {
            // Arrange
            var request = new CriarTicketRequest
            {
                Titulo = "Ticket Válido",
                Descricao = "Descrição válida",
                UsuarioId = 1
            };

            // Act
            var resultado = await _validator.ValidateAsync(request);

            // Assert
            Assert.True(resultado.IsValid);
        }

        [Fact]
        public async Task Validar_SemTitulo_DeveRetornarErro()
        {
            // Arrange
            var request = new CriarTicketRequest
            {
                Titulo = "",
                Descricao = "Descrição",
                UsuarioId = 1
            };

            // Act
            var resultado = await _validator.ValidateAsync(request);

            // Assert
            Assert.False(resultado.IsValid);
            Assert.Contains("Título é obrigatório", resultado.Errors.Select(e => e.ErrorMessage));
        }
    }
}
```

## ⚙️ Dependencies

Este projeto deve referenciar:
- 📦 **Domínio**: Para testar entidades
- 📦 **Serviço**: Para testar serviços
- 📦 **Infra**: Para testar repositórios (opcional)
- 📦 `xUnit` - Framework de testes
- 📦 `Moq` - Para criar mocks
- 📦 `FluentAssertions` - Para assertions mais legíveis

## 🔒 Padrão AAA (Arrange, Act, Assert)

Todos os testes devem seguir o padrão AAA:

1. **Arrange**: Preparar dados e mocks necessários
2. **Act**: Executar a ação a ser testada
3. **Assert**: Validar os resultados

```csharp
[Fact]
public void Exemplo_DoMetodo_DeveComportarseAssim()
{
    // Arrange - Preparação
    var entrada = "teste";
    
    // Act - Execução
    var resultado = funcao(entrada);
    
    // Assert - Validação
    Assert.Equal("esperado", resultado);
}
```

## 📊 Executar Testes

### Via Terminal
```bash
# Executar todos os testes
dotnet test

# Executar com cobertura
dotnet test /p:CollectCoverage=true

# Executar testes específicos
dotnet test --filter "ClassName~TicketServiceTests"
```

### Via VS Code
- Abra o projeto de testes
- Procure no Explorer por "Test Explorer"
- Clique em um teste para executar

## 🎯 Boas Práticas

1. **Um teste por comportamento**: Cada teste valida um comportamento específico
2. **Nomes descritivos**: `MetodoTestado_Condicao_ResultadoEsperado()`
3. **Testes isolados**: Um teste não deve depender de outro
4. **Mocks em vez de banco real**: Use Moq para simular dependências
5. **Rápidos**: Testes devem executar em milissegundos
6. **DRY**: Reutilize fixtures para evitar repetição
7. **Cobertura**: Aim > 80% de cobertura de código crítico

## 📚 Referências

- [xUnit Documentation](https://xunit.net/)
- [Moq Library](https://github.com/moq/moq4)
- [FluentAssertions](https://www.fluentassertions.com/)
- [Unit Testing Best Practices](https://docs.microsoft.com/en-us/dotnet/core/testing/)

---

**Resumo**: Este projeto garante que o código está funcionando corretamente. Test-driven development (TDD) é uma prática recomendada.
