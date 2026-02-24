# API.ConectaAtende.Dominio

Projeto de **Camada de Domínio** da arquitetura da API ConectaAtende.

## 📋 Descrição

A camada de Domínio é responsável por **conter as entidades principais do negócio e as regras fundamentais da aplicação**. Este projeto define os objetos (modelos) que representam os conceitos-chave do sistema, como Tickets, Usuários, Atendimentos, etc.

## 🎯 Responsabilidades

- ✅ **Definir Entidades**: Classes que representam as tabelas do banco de dados (ex: `Ticket`, `Usuario`, `Atendimento`)
- ✅ **Valores de Domínio**: Objetos que representam valores específicos do negócio (ex: `Status`, `Prioridade`)
- ✅ **Agregados**: Agrupar entidades relacionadas que formam um conjunto lógico
- ✅ **Validações de Negócio**: Regras que devem ser sempre respeitadas (ex: um ticket não pode ser criado sem titulo)
- ✅ **Interfaces de Repositório**: Contratos para operações de persistência (o repositório real fica na Infra)

## ⚙️ Dependencies

Este projeto **NÃO deve ter dependências** de camadas inferiores (Infra, Serviço). Pode referenciar:
- Bibliotecas .NET padrão
- Packages NuGet para validação (ex: `FluentValidation`)

## 🔒 Regras de Ouro

1. **Não referencie outras camadas**: Não importe de Infra, Serviço ou Controllers
2. **Foco no negócio**: Código aqui representa regras do negócio, não detalhes técnicos
3. **Sem dependências de banco**: Não coloque `DbContext` ou Entity Framework aqui
4. **Validações rigorosas**: Garanta que as entidades sejam sempre válidas
5. **Imutabilidade quando possível**: Use `readonly` para propriedades que não mudam

## 📚 Referências

- [Domain-Driven Design (DDD)](https://martinfowler.com/bliki/DomainDrivenDesign.html)
- [Entidades x Value Objects](https://martinfowler.com/bliki/ValueObject.html)

---

**Resumo**: Este projeto é o "coração" da aplicação. Define COMO o negócio funciona.
