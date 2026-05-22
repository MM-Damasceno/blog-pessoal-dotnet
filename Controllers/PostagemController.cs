using BlogPessoal.Models;
using BlogPessoal.Repositories;
using BlogPessoal.Services.IA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers
{
    [ApiController]
    [Route("api/postagens")]
    [Authorize]
    public class PostagemController : ControllerBase
    {
        private readonly IPostagemRepository _repo;
        private readonly IIAService          _iaService;

        public PostagemController(IPostagemRepository repo, IIAService iaService)
        {
            _repo      = repo;
            _iaService = iaService;
        }

        // GET /api/postagens
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _repo.GetAllAsync());

        // GET /api/postagens/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var postagem = await _repo.GetByIdAsync(id);
            return postagem == null ? NotFound() : Ok(postagem);
        }

        // GET /api/postagens/filtro?titulo=abc
        [HttpGet("filtro")]
        public async Task<IActionResult> GetByTitulo([FromQuery] string titulo) =>
            Ok(await _repo.GetByTituloAsync(titulo));

        // GET /api/postagens/usuario/{usuarioId}
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuario(long usuarioId) =>
            Ok(await _repo.GetByUsuarioAsync(usuarioId));

        // GET /api/postagens/tema/{temaId}
        [HttpGet("tema/{temaId}")]
        public async Task<IActionResult> GetByTema(long temaId) =>
            Ok(await _repo.GetByTemaAsync(temaId));

        // POST /api/postagens
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Postagem postagem)
        {
            // Enriquecer com IA antes de salvar
            var resultado = await _iaService.GerarResumoAsync(postagem.Texto);
            postagem.ResumoIA    = resultado.Resumo;
            postagem.TagsIA      = resultado.Tags;
            postagem.CategoriaIA = resultado.Categoria;

            var criada = await _repo.CreateAsync(postagem);
            return CreatedAtAction(nameof(GetById), new { id = criada.Id }, criada);
        }

        // PUT /api/postagens/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Postagem postagem)
        {
            postagem.Id = id;
            var atualizada = await _repo.UpdateAsync(postagem);
            return atualizada == null ? NotFound() : Ok(atualizada);
        }

        // DELETE /api/postagens/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var sucesso = await _repo.DeleteAsync(id);
            return sucesso ? NoContent() : NotFound();
        }
    }
}
