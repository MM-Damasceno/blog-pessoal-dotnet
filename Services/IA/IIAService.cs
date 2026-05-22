using BlogPessoal.DTOs;

namespace BlogPessoal.Services.IA
{
    public interface IIAService
    {
        Task<ResultadoIA> GerarResumoAsync(string conteudo);
    }
}
