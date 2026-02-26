using API.ConectaAtende.Aplicacao.Dtos;
using API.ConectaAtende.Aplicacao.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("contatos/v1")]
public class ContatosController : ControllerBase
{
    private readonly IServicoContatos _servicoContatos;

    public ContatosController(IServicoContatos servicoContatos)
    {
        _servicoContatos = servicoContatos;
    }

    /// <summary>
    /// Cria um novo contato com base nos dados fornecidos no corpo da requisição.
    /// </summary>
    /// <param name="contato"></param>
    /// <returns></returns>
    [HttpPost("adiciona-contato")]
    public async Task<IActionResult> CriaContato([FromBody] ContatosDto contato)
    {
        var novoContato = await _servicoContatos.CriaContato(contato);
        if (novoContato == null)
            return BadRequest("Não foi possível criar o contato.");

        return Ok(novoContato);
    }

    /// <summary>
    /// Retorna uma lista de todos os contatos existentes.
    /// </summary>
    /// <returns></returns>
    [HttpGet("busca-todos")]
    public async Task<IActionResult> ObterTodos()
    {
        var contatos = await _servicoContatos.ObterTodos();
        if (contatos == null)
            return NotFound();
        if (!contatos.Any())
            return NoContent();

        return Ok(contatos);
    }

    /// <summary>
    /// Retorna os detalhes de um contato específico com base no ID fornecido na URL.
    /// </summary>
    /// <param name="idContato"></param>
    /// <returns></returns>
    [HttpGet("idContato/{idContato}")]
    public async Task<IActionResult> ObterPorId(Guid idContato)
    {
        var contato = await _servicoContatos.ObterPorId(idContato);
        if (contato == null)
            return NotFound("Contato não encontrado.");

        return Ok(contato);
    }

    /// <summary>
    /// Atualiza os detalhes de um contato existente com base no ID fornecido na URL e nos dados fornecidos no corpo da requisição.
    /// </summary>
    /// <param name="idContato"></param>
    /// <param name="contato"></param>
    /// <returns></returns>
    [HttpPut("atualiza/idContato/{idContato}")]
    public async Task<IActionResult> Atualizar(Guid idContato, [FromBody] ContatosDto contato)
    {
        var contatoAtualizado = await _servicoContatos.Atualizar(idContato, contato);
        if (contatoAtualizado == null)
            return BadRequest("Não foi possível atualizar o contato.");

        return Ok(contatoAtualizado);
    }

    /// <summary>
    /// Exclui um contato existente com base no ID fornecido na URL.
    /// </summary>
    /// <param name="idContato"></param>
    /// <returns></returns>
    [HttpDelete("remove/idContato/{idContato}")]
    public async Task<IActionResult> Excluir(Guid idContato)
    {
        await _servicoContatos.Excluir(idContato);
        return Ok("Contato excluído com sucesso.");
    }

    /// <summary>
    /// Retorna uma lista paginada de contatos com base nos parâmetros de página e quantidade fornecidos na URL.
    /// </summary>
    /// <param name="paginaContatos"></param>
    /// <param name="qntdContatos"></param>
    /// <returns></returns>
    [HttpGet("lista-contatos")]
    public async Task<IActionResult> Listar(int paginaContatos = 1, int qntdContatos = 10)
    {
        var contatos = await _servicoContatos.Listar(paginaContatos, qntdContatos);
        if (contatos == null)
            return NotFound("Nenhum contato encontrado para os parâmetros fornecidos.");

        return Ok(contatos);
    }
}