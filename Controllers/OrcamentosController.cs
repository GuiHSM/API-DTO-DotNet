using Microsoft.AspNetCore.Mvc;
using api.ModelViews;
using api.Models;
using api.DTOs;
using api.Servicos;

namespace api.Controllers;

[Route("orcamentos")]
public class OrcamentosController : ControllerBase
{
    private OrcamentoRepositorioMySql _servico;
    public OrcamentosController(OrcamentoRepositorioMySql servico)
    {
        _servico = servico;
    }
    // GET: Orcamentos
    [HttpGet("/Orcamentos")]
    public async Task<IActionResult> IndexO()
    {
        var orcamentos = await _servico.TodosAsync();
        return StatusCode(200, orcamentos);
    }

    [HttpGet("/Orcamentos/{id}")]
    public async Task<IActionResult> DetailsO([FromRoute] int id)
    {
       var orcamento = (await _servico.TodosAsync()).Find(c => c.Id == id);

        return StatusCode(200, orcamento);
    }

    
    // POST: Orcamentos
    [HttpPost("/Orcamentos")]
    public async Task<IActionResult> CreateO([FromBody] OrcamentoDTO orcamentoDTO)
    {
        var orcamento = BuilderServico<Orcamento>.Builder(orcamentoDTO);
        await _servico.IncluirAsync(orcamento);
        return StatusCode(201, orcamento);
    }


    // PUT: Orcamentos/5
    [HttpPut("/Orcamentos/{id}")]
    public async Task<IActionResult> UpdateO([FromRoute] int id, [FromBody] Orcamento orcamento)
    {
        if (id != orcamento.Id)
        {
            return StatusCode(400, new {
                Mensagem = "O Id do orcamento precisa bater com o id da URL"
            });
        }

        var orcamentoDb = await _servico.AtualizarAsync(orcamento);

        return StatusCode(200, orcamentoDb);
    }

    // POST: Orcamentos/5
    [HttpDelete("/Orcamentos/{id}")]
    public async Task<IActionResult> DeleteO([FromRoute] int id)
    {
        var orcamentoDb = (await _servico.TodosAsync()).Find(c => c.Id == id);
        if(orcamentoDb is null)
        {
            return StatusCode(404, new {
                Mensagem = "O orcamento informado não existe"
            });
        }

        await _servico.ApagarAsync(orcamentoDb);

        return StatusCode(204);
    }
}