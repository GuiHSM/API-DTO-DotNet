using Microsoft.AspNetCore.Mvc;
using api.ModelViews;
using api.Models;
using api.DTOs;
using api.Servicos;

namespace api.Controllers;

[Route("pessoasJuridicas")]
public class PessoasJuridicasController : ControllerBase
{
    private PessoaJuridicaRepositorioMySql _servico;
    public PessoasJuridicasController(PessoaJuridicaRepositorioMySql servico)
    {
        _servico = servico;
    }
    // GET: PessoasJuridicas
    [HttpGet("/pessoasJuridicas")]
    public async Task<IActionResult> IndexPJ()
    {
        var pessoasJuridicas = await _servico.TodosAsync();
        return StatusCode(200, pessoasJuridicas);
    }

    [HttpGet("/pessoasJuridicas/{id}")]
    public async Task<IActionResult> DetailsPJ([FromRoute] int id)
    {
       var pessoafisica = (await _servico.TodosAsync()).Find(c => c.Id == id);

        return StatusCode(200, pessoafisica);
    }

    
    // POST: PessoasJuridicas
    [HttpPost("/pessoasJuridicas")]
    public async Task<IActionResult> CreatePJ([FromBody] PessoaJuridicaDTO pessoafisicaDTO)
    {
        var pessoafisica = BuilderServico<PessoaJuridica>.Builder(pessoafisicaDTO);
        await _servico.IncluirAsync(pessoafisica);
        return StatusCode(201, pessoafisica);
    }


    // PUT: PessoasJuridicas/5
    [HttpPut("/pessoasJuridicas/{id}")]
    public async Task<IActionResult> UpdatePJ([FromRoute] int id, [FromBody] PessoaJuridica pessoafisica)
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

    // POST: PessoasJuridicas/5
    [HttpDelete("/pessoasJuridicas/{id}")]
    public async Task<IActionResult> DeletePJ([FromRoute] int id)
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