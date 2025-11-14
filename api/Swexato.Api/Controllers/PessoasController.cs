using Microsoft.AspNetCore.Mvc;
using Swexato.Api.Data;
using Swexato.Api.Models;
using Swexato.Api.Services;

namespace Swexato.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly PessoaService _service;
        private readonly AppDbContext _db;

        public PessoasController(PessoaService service, AppDbContext db)
        {
            _service = service;
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Pessoa p)
        {
            try
            {
                var created = await _service.CriarAsync(p);
                return CreatedAtAction(nameof(ObterPorId), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar() => Ok(await _db.Pessoas.ToListAsync());

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var p = await _db.Pessoas.FindAsync(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] Pessoa updated)
        {
            var p = await _db.Pessoas.FindAsync(id);
            if (p == null) return NotFound();

            if (!CpfValidator.Validar(updated.CPF)) return BadRequest(new { error = "CPF inválido." });

            p.Nome = updated.Nome;
            p.CPF = updated.CPF;
            p.Email = updated.Email;
            p.DataNascimento = updated.DataNascimento;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var p = await _db.Pessoas.FindAsync(id);
            if (p == null) return NotFound();
            _db.Pessoas.Remove(p);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
