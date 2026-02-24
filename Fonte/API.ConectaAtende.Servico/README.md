# API.ConectaAtende.Servico

Projeto de **Camada de Serviço** (Application/Business Logic) da arquitetura da API ConectaAtende.

## 📋 Descrição

A camada de Serviço é responsável por **coordenar a lógica de aplicação entre o Domínio e a Infraestrutura**. Este projeto contém os serviços que orquestram operações de negócio, aplicam validações complexas, coordenam múltiplos repositórios e preparam dados para apresentação.

## 🎯 Responsabilidades

- ✅ **Orquestração de Negócio**: Coordenar operações que envolvem múltiplas entidades
- ✅ **DTOs (Data Transfer Objects)**: Definir objetos para transferência de dados entre camadas
- ✅ **Validações Complexas**: Validações que envolvem múltiplas entidades ou chamadas a repositórios
- ✅ **Transformação de Dados**: Converter entre Entidades e DTOs
- ✅ **Tratamento de Exceções**: Converter exceções de domínio em mensagens amigáveis
- ✅ **Testes de Lógica**: Código facilmente testável sem dependências de banco

## 📁 Estrutura Esperada

```
API.ConectaAtende.Servico/
├── DTOs/                                 # Transfer Objects
│   ├── Request/
│   │   ├── CriarTicketRequest.cs        # DTO para criar ticket
│   │   ├── AtualizarTicketRequest.cs    # DTO para atualizar ticket
│   │   └── ...
│   └── Response/
│       ├── TicketResponse.cs            # DTO de resposta de ticket
│       ├── UsuarioResponse.cs           # DTO de resposta de usuario
│       └── ...
├── Services/
│   ├── TicketService.cs                 # Serviço de Ticket
│   ├── UsuarioService.cs                # Serviço de Usuario
│   ├── ITicketService.cs                # Interface de Serviço
│   ├── IUsuarioService.cs               # Interface de Serviço
│   └── ...
├── Validators/
│   ├── TicketValidator.cs               # Validadores com FluentValidation
│   ├── UsuarioValidator.cs
│   └── ...
├── Mappings/
│   ├── MappingProfile.cs                # AutoMapper profile
│   └── ...
└── README.md                            # Este arquivo
```

## 💡 Exemplos de Implementação

### DTO Request
```csharp
namespace API.ConectaAtende.Servico.DTOs.Request
{
    public class CriarTicketRequest
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int UsuarioId { get; set; }
    }
}
```

### DTO Response
```csharp
namespace API.ConectaAtende.Servico.DTOs.Response
{
    public class TicketResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
```

### Interface de Serviço
```csharp
namespace API.ConectaAtende.Servico.Services
{
    public interface ITicketService
    {
        Task<TicketResponse> ObterPorIdAsync(int id);
        Task<IEnumerable<TicketResponse>> ObterTodosAsync();
        Task<TicketResponse> CriarAsync(CriarTicketRequest request);
        Task<TicketResponse> AtualizarAsync(int id, AtualizarTicketRequest request);
        Task DeletarAsync(int id);
    }
}
```

### Implementação de Serviço
```csharp
namespace API.ConectaAtende.Servico.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repository;
        private readonly IMapper _mapper;
        
        public TicketService(ITicketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<TicketResponse> ObterPorIdAsync(int id)
        {
            var ticket = await _repository.ObterPorIdAsync(id);
            
            if (ticket == null)
                throw new TicketNaoEncontradoException($"Ticket {id} não encontrado");
                
            return _mapper.Map<TicketResponse>(ticket);
        }
        
        public async Task<TicketResponse> CriarAsync(CriarTicketRequest request)
        {
            // Validar request
            if (string.IsNullOrEmpty(request.Titulo))
                throw new ArgumentException("Título é obrigatório");
            
            // Criar entidade de domínio
            var ticket = new Ticket(request.Titulo, request.Descricao, request.UsuarioId);
            
            // Persistir
            await _repository.CriarAsync(ticket);
            
            // Retornar DTO
            return _mapper.Map<TicketResponse>(ticket);
        }
    }
}
```

### Validador (FluentValidation)
```csharp
namespace API.ConectaAtende.Servico.Validators
{
    public class CriarTicketValidator : AbstractValidator<CriarTicketRequest>
    {
        public CriarTicketValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("Título é obrigatório")
                .MaximumLength(255).WithMessage("Título não pode ter mais que 255 caracteres");
                
            RuleFor(x => x.UsuarioId)
                .GreaterThan(0).WithMessage("UsuarioId inválido");
        }
    }
}
```

### AutoMapper Profile
```csharp
namespace API.ConectaAtende.Servico.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ticket, TicketResponse>();
            CreateMap<CriarTicketRequest, Ticket>().ConstructUsing(src => 
                new Ticket(src.Titulo, src.Descricao, src.UsuarioId));
        }
    }
}
```

## ⚙️ Dependencies

Este projeto deve referenciar:
- 📦 **Domínio**: Para trabalhar com entidades e interfaces
- 📦 **Infra**: Para usar repositórios (opcional, via DI)
- 📦 `AutoMapper` - Para mapeamento entre objetos
- 📦 `FluentValidation` - Para validações em cadeia

**Não deve referenciar**:
- ❌ Controllers
- ❌ Camada de apresentação

## 🔒 Regras de Ouro

1. **DTOs para comunicação**: Nunca retorne entidades diretas ao Controller
2. **Validações aqui**: Antes de chamar repositório, valide
3. **Mapear sempre**: Use AutoMapper para converter entre Entidades e DTOs
4. **Serviços são orquestradores**: Não coloque muita complexidade em um serviço
5. **Sem dependência de HTTP**: Serviços não conhecem sobre Controllers ou HTTP
6. **Async por padrão**: Sempre use async/await para operações I/O

## 📚 Configuração Padrão

### Registrar no Startup.cs
```csharp
// AutoMapper
services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Validadores
services.AddScoped<IValidator<CriarTicketRequest>, CriarTicketValidator>();

// Serviços
services.AddScoped<ITicketService, TicketService>();
services.AddScoped<IUsuarioService, UsuarioService>();
```

## 📚 Referências

- [Application Service Pattern](https://www.baeldung.com/design-patterns-architectural)
- [Data Transfer Object (DTO)](https://martinfowler.com/eaaCatalog/dataTransferObject.html)
- [AutoMapper Documentation](https://automapper.org/)
- [FluentValidation](https://fluentvalidation.net/)

---

**Resumo**: Este projeto é o "gerenciador" da aplicação. Coordena COMO as operações acontecem e os dados fluem.
