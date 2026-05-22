using BlogPessoal.Services.IA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers
{
    [ApiController]
    [Route("api/ia")]
    [Authorize]
    public class IAController : ControllerBase
    {
        private readonly IIAService _iaService;

        public IAController(IIAService iaService)
        {
            _iaService = iaService;
        }

        // POST /api/ia/resumir
        [HttpPost("resumir")]
        public async Task<IActionResult> Resumir([FromBody] string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return BadRequest(new { mensagem = "Texto não pode ser vazio." });

            var resultado = await _iaService.GerarResumoAsync(texto);
            return Ok(resultado);
        }
    }
}
