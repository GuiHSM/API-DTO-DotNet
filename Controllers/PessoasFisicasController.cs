using Microsoft.AspNetCore.Mvc;
using api.ModelViews;
using api.Models;
using api.DTOs;
using api.Servicos;

namespace api.Controllers;

[Route("pessoasFisicas")]
public class PessoasFisicasController : ControllerBase
{
    private PessoaFisicaRepositorioMySql _servico;
    public PessoasFisicasController(PessoaFisicaRepositorioMySql servico)
    {
        _servico = servico;
    }
    // GET: PessoasFisicas
    [HttpGet("/pessoasFisicas")]
    public async Task<IActionResult> Index()
    {
        var pessoasFisicas = await _servico.TodosAsync();
        return StatusCode(200, pessoasFisicas);
    }

    [HttpGet("/pessoasFisicas/{id}")]
    public async Task<IActionResult> Details([FromRoute] int id)
    {
       var pessoafisica = (await _servico.TodosAsync()).Find(c => c.Id == id);

        return StatusCode(200, pessoafisica);
    }

    
    // POST: PessoasFisicas
    [HttpPost("/pessoasFisicas")]
    public async Task<IActionResult> Create([FromBody] PessoaFisicaDTO pessoafisicaDTO)
    {
        var pessoafisica = BuilderServico<PessoaFisica>.Builder(pessoafisicaDTO);
        await _servico.IncluirAsync(pessoafisica);
        return StatusCode(201, pessoafisica);
    }


    // PUT: PessoasFisicas/5
    [HttpPut("/pessoasFisicas/{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PessoaFisica pessoafisica)
    {
        if (id != pessoafisica.Id)
        {
            return StatusCode(400, new {
                Mensagem = "O Id do pessoafisica precisa bater com o id da URL"
            });
        }

        var pessoafisicaDb = await _servico.AtualizarAsync(pessoafisica);

        return StatusCode(200, pessoafisicaDb);
    }

    // POST: PessoasFisicas/5
    [HttpDelete("/pessoasFisicas/{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var pessoafisicaDb = (await _servico.TodosAsync()).Find(c => c.Id == id);
        if(pessoafisicaDb is null)
        {
            return StatusCode(404, new {
                Mensagem = "O pessoafisica informado não existe"
            });
        }

        await _servico.ApagarAsync(pessoafisicaDb);

        return StatusCode(204);
    }
}