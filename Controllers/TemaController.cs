using BlogPessoal.Models;
using BlogPessoal.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers
{
    [ApiController]
    [Route("api/temas")]
    [Authorize]
    public class TemaController : ControllerBase
    {
        private readonly ITemaRepository _repo;

        public TemaController(ITemaRepository repo)
        {
            _repo = repo;
        }

        // GET /api/temas
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _repo.GetAllAsync());

        // GET /api/temas/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var tema = await _repo.GetByIdAsync(id);
            return tema == null ? NotFound() : Ok(tema);
        }

        // GET /api/temas/filtro?descricao=abc
        [HttpGet("filtro")]
        public async Task<IActionResult> GetByDescricao([FromQuery] string descricao) =>
            Ok(await _repo.GetByDescricaoAsync(descricao));

        // POST /api/temas
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tema tema)
        {
            var criado = await _repo.CreateAsync(tema);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        // PUT /api/temas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Tema tema)
        {
            tema.Id = id;
            var atualizado = await _repo.UpdateAsync(tema);
            return atualizado == null ? NotFound() : Ok(atualizado);
        }

        // DELETE /api/temas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var sucesso = await _repo.DeleteAsync(id);
            return sucesso ? NoContent() : NotFound();
        }
    }
}
