# ConectaAtende

API REST desenvolvida em ASP.NET Core para o sistema Conecta Atende, um projeto acadêmico para gerenciamento de atendimentos e tickets.

## 🚀 Tecnologias Utilizadas

- **.NET 8.0** - Framework principal
- **ASP.NET Core** - Para construção da API
- **Swagger/OpenAPI** - Documentação interativa da API
- **Entity Framework Core** - ORM para acesso a dados (se aplicável)
- **xUnit** - Framework de testes unitários
- **AWS Lambda** - Suporte para deploy serverless

## 📁 Estrutura do Projeto

```
ConectaAtende/
├── Fonte/
│   ├── API.ConectaAtende/          # Projeto principal da API
│   ├── API.ConectaAtende.Dominio/  # Camada de domínio
│   ├── API.ConectaAtende.Infra/    # Camada de infraestrutura
│   ├── API.ConectaAtende.Servico/  # Camada de serviços
│   ├── API.ConectaAtende.Utilitario/ # Utilitários
│   └── API.ConectaAtende.TestesUnitarios/ # Testes unitários
├── ConectaAtende.sln               # Solução Visual Studio
└── README.md                       # Este arquivo
```

## 🛠️ Como Executar

### Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio Code](https://code.visualstudio.com/) ou [Visual Studio](https://visualstudio.microsoft.com/)

### Execução Local

1. **Clone o repositório**:
   ```bash
   git clone <url-do-repositorio>
   cd ConectaAtende
   ```

2. **Restaure as dependências**:
   ```bash
   dotnet restore
   ```

3. **Execute a aplicação**:
   ```bash
   # Via terminal
   dotnet run --project Fonte/API.ConectaAtende/API.ConectaAtende.csproj

   # Ou via VS Code (F5)
   # Selecione "C#: API.ConectaAtende" no Run and Debug
   ```

4. **Acesse a API**:
   - **Swagger UI**: `http://localhost:5000/swagger` ou `https://localhost:5001/swagger`
   - **Endpoint raiz**: `http://localhost:5000/` (retorna mensagem de boas-vindas)

### Testes

Execute os testes unitários:
```bash
dotnet test
```

## 📚 Documentação da API

A documentação completa da API está disponível via Swagger. Após iniciar a aplicação, acesse `/swagger` para explorar os endpoints disponíveis.

### Endpoints Principais

- `GET /` - Mensagem de boas-vindas
- Outros endpoints conforme implementação nos controllers

## 🏗️ Arquitetura

O projeto segue uma arquitetura em camadas:

- **API**: Camada de apresentação (Controllers, Startup)
- **Domínio**: Regras de negócio e entidades
- **Infraestrutura**: Acesso a dados e integrações externas
- **Serviços**: Lógica de aplicação
- **Utilitários**: Funções auxiliares
- **Testes**: Cobertura de testes unitários

## 🤝 Como Contribuir

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/nova-feature`)
3. Commit suas mudanças (`git commit -am 'Adiciona nova feature'`)
4. Push para a branch (`git push origin feature/nova-feature`)
5. Abra um Pull Request

## 📝 Licença

Este projeto é parte de um trabalho acadêmico e não possui licença específica.

## 👥 Autor

Desenvolvido como trabalho acadêmico para a faculdade.

---

**Nota**: Este projeto foi desenvolvido com fins educacionais e pode conter configurações específicas para ambiente AWS Lambda.
