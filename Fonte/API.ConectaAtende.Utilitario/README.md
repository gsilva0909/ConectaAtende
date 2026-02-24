# API.ConectaAtende.Utilitario

Projeto de **Classes Utilitárias e Auxiliares** da arquitetura da API ConectaAtende.

## 📋 Descrição

A camada de Utilitários é responsável por **fornecer funções auxiliares, extensões e ferramentas reutilizáveis** que não se encaixam especificamente em nenhuma outra camada. Este projeto contém código comum que pode ser usado em qualquer lugar da aplicação.

## 🎯 Responsabilidades

- ✅ **Métodos de Extensão**: Estender funcionalidades de tipos .NET padrão
- ✅ **Helpers**: Funções auxiliares para tarefas comuns
- ✅ **Constantes**: Valores que são reutilizados em vários pontos
- ✅ **Enums Globais**: Enumerações compartilhadas por toda a aplicação
- ✅ **Conversores**: Conversor de tipos e formatos
- ✅ **Criptografia/Hash**: Funções de segurança e criptografia
- ✅ **Formatadores**: Formatação de datas, moeda, strings, etc

## 📁 Estrutura Esperada

```
API.ConectaAtende.Utilitario/
├── Constants/
│   ├── MensagensConstantes.cs           # Mensagens usadas em toda app
│   ├── NumericosConstantes.cs           # Valores numéricos constantes
│   └── ...
├── Extensions/
│   ├── StringExtensions.cs              # Extensões para string
│   ├── DateTimeExtensions.cs            # Extensões para DateTime
│   ├── ListExtensions.cs                # Extensões para listas
│   └── ...
├── Helpers/
│   ├── CriptografiaHelper.cs            # Hash de senhas, etc
│   ├── FormatacaoHelper.cs              # Formatação de datas, moeda
│   ├── ValidacaoHelper.cs               # Validações comuns (CPF, email)
│   └── ...
├── Conversores/
│   ├── StatusConversor.cs               # Converter entre tipos
│   ├── DateTimeConversor.cs             # Converter DateTime
│   └── ...
├── Enums/
│   ├── TipoOperacao.cs
│   ├── NivelPermissao.cs
│   └── ...
└── README.md                            # Este arquivo
```

## 💡 Exemplos de Implementação

### Constantes
```csharp
namespace API.ConectaAtende.Utilitario.Constants
{
    public static class MensagensConstantes
    {
        public const string TICKET_NAO_ENCONTRADO = "Ticket não foi encontrado";
        public const string USUARIO_NAO_ENCONTRADO = "Usuário não foi encontrado";
        public const string ERRO_CRIACAO_TICKET = "Erro ao criar ticket";
        public const string OPERACAO_CONCLUIDA = "Operação realizada com sucesso";
    }

    public static class PaginacaoConstantes
    {
        public const int ITENS_POR_PAGINA_PADRAO = 10;
        public const int ITENS_POR_PAGINA_MAXIMO = 100;
    }
}
```

### Extensões
```csharp
namespace API.ConectaAtende.Utilitario.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Remove caracteres especiais de uma string
        /// </summary>
        public static string RemoverCaracteresEspeciais(this string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;
                
            return Regex.Replace(texto, @"[^a-zA-Z0-9\s]", "");
        }

        /// <summary>
        /// Capitaliza a primeira letra de uma string
        /// </summary>
        public static string CapitalizarPrimeira(this string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;
                
            return char.ToUpper(texto[0]) + texto.Substring(1).ToLower();
        }

        /// <summary>
        /// Verifica se string é um email válido
        /// </summary>
        public static bool EhEmailValido(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }

    public static class DateTimeExtensions
    {
        /// <summary>
        /// Retorna a idade baseada em data de nascimento
        /// </summary>
        public static int CalcularIdade(this DateTime dataNascimento)
        {
            var hoje = DateTime.Now;
            var idade = hoje.Year - dataNascimento.Year;

            if (dataNascimento.Date > hoje.AddYears(-idade))
                idade--;

            return idade;
        }

        /// <summary>
        /// Formata DateTime em string PT-BR
        /// </summary>
        public static string FormatarPtBr(this DateTime data)
        {
            return data.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.GetCultureInfo("pt-BR"));
        }
    }
}
```

### Helpers
```csharp
namespace API.ConectaAtende.Utilitario.Helpers
{
    public static class CriptografiaHelper
    {
        /// <summary>
        /// Gera hash SHA256 de uma string
        /// </summary>
        public static string GerarHash(string texto)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        /// <summary>
        /// Verifica se um texto corresponde a um hash
        /// </summary>
        public static bool VerificarHash(string texto, string hash)
        {
            var hashDoTexto = GerarHash(texto);
            return hashDoTexto.Equals(hash);
        }
    }

    public static class ValidacaoHelper
    {
        /// <summary>
        /// Valida se é um CPF válido
        /// </summary>
        public static bool EhCpfValido(string cpf)
        {
            cpf = cpf?.Replace(".", "").Replace("-", "").Trim();

            if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
                return false;

            // Validação com dígitos verificadores...
            // (implementação simplificada)
            return cpf.All(char.IsDigit);
        }
    }
}
```

### Conversores
```csharp
namespace API.ConectaAtende.Utilitario.Conversores
{
    public static class StatusConversor
    {
        public static string ConverterParaTexto(Status status)
        {
            return status switch
            {
                Status.Aberto => "Aberto",
                Status.EmAtendimento => "Em Atendimento",
                Status.Resolvido => "Resolvido",
                Status.Fechado => "Fechado",
                _ => "Desconhecido"
            };
        }

        public static Status ConverterDeTexto(string statusTexto)
        {
            return statusTexto?.ToLower() switch
            {
                "aberto" => Status.Aberto,
                "anatendimento" => Status.EmAtendimento,
                "resolvido" => Status.Resolvido,
                "fechado" => Status.Fechado,
                _ => throw new ArgumentException($"Status inválido: {statusTexto}")
            };
        }
    }
}
```

### Enums
```csharp
namespace API.ConectaAtende.Utilitario.Enums
{
    public enum TipoOperacao
    {
        Criar = 1,
        Ler = 2,
        Atualizar = 3,
        Deletar = 4
    }

    public enum NivelPermissao
    {
        Visitante = 1,
        Usuario = 2,
        Gerenciador = 3,
        Administrador = 4
    }
}
```

## ⚙️ Dependencies

Este projeto deve ser **independente e não ter dependências** de outras camadas. Pode usar apenas:
- 📦 Bibliotecas .NET padrão
- 📦 Pequenas bibliotecas externas de uso comum (ex: `System.IdentityModel.Tokens.Jwt`)

## 🔒 Regras de Ouro

1. **Seja agnóstico**: Código aqui não deve depender de particularidades de outras camadas
2. **Reutilizável**: Qualquer classe da aplicação deve poder usar
3. **Simples e direto**: Funções pequenas, bem documentadas
4. **Sem efeitos colaterais**: Funções devem ser puras quando possível
5. **Testável**: Código sem dependências complexas é fácil de testar
6. **Bem documentado**: Use XMLDoc para documentar métodos públicos

## 📚 Exemplo de Documentação (XMLDoc)

```csharp
/// <summary>
/// Remove caracteres especiais de uma string.
/// </summary>
/// <param name="texto">A string de entrada para processar</param>
/// <returns>String sem caracteres especiais</returns>
/// <example>
/// <code>
/// var resultado = "Olá, Mundo!".RemoverCaracteresEspeciais();
/// // resultado: "Olá Mundo"
/// </code>
/// </example>
public static string RemoverCaracteresEspeciais(this string texto)
{
    // implementação...
}
```

---

**Resumo**: Este projeto é a "caixa de ferramentas" da aplicação. Fornece funções úteis e comuns reutilizáveis em qualquer lugar.
