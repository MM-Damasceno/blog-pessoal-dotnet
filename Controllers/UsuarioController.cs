using BlogPessoal.DTOs;
using BlogPessoal.Models;
using BlogPessoal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        // POST /api/usuarios/cadastrar
        [HttpPost("cadastrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioCadastroDTO dto)
        {
            var usuario = await _service.CadastrarAsync(dto);
            if (usuario == null)
                return BadRequest(new { mensagem = "Email já cadastrado." });

            return CreatedAtAction(nameof(Cadastrar), new { id = usuario.Id }, usuario);
        }

        // POST /api/usuarios/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UsuarioLogin login)
        {
            var token = await _service.LoginAsync(login);
            if (token == null)
                return Unauthorized(new { mensagem = "Email ou senha inválidos." });

            return Ok(new { token });
        }

        // PUT /api/usuarios/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, [FromBody] UsuarioCadastroDTO dto)
        {
            var usuario = await _service.AtualizarAsync(id, dto);
            if (usuario == null)
                return NotFound(new { mensagem = "Usuário não encontrado ou email já em uso." });

            return Ok(usuario);
        }

        // DELETE /api/usuarios/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Deletar(long id)
        {
            var sucesso = await _service.DeletarAsync(id);
            if (!sucesso)
                return NotFound(new { mensagem = "Usuário não encontrado." });

            return NoContent();
        }
    }
}
