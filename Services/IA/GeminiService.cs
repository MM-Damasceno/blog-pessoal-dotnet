using BlogPessoal.DTOs;
using System.Text;
using System.Text.Json;

namespace BlogPessoal.Services.IA
{
    public class GeminiService : IIAService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;
        private readonly ILogger<GeminiService> _logger;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public GeminiService(
            HttpClient http,
            IConfiguration config,
            ILogger<GeminiService> logger)
        {
            _http   = http;
            _config = config;
            _logger = logger;
        }

        public async Task<ResultadoIA> GerarResumoAsync(string conteudo)
        {
            try
            {
                var apiKey = _config["Gemini:ApiKey"]!;
                var prompt = PromptBuilder.BuildResumoPrompt(conteudo);

                var body = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[] { new { text = prompt } }
                        }
                    },
                    generationConfig = new
                    {
                        temperature     = 0.3,
                        maxOutputTokens = 400
                    }
                };

                var json    = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

              var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";
                var response = await _http.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                var respJson = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(respJson);

                // Extrai o texto da resposta do Gemini
                var texto = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString() ?? "{}";

                // Desserializa o JSON retornado pela IA
                var resultado = JsonSerializer.Deserialize<ResultadoIA>(texto, _jsonOptions);
                return resultado ?? new ResultadoIA();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao chamar API Gemini");
                _logger.LogError("Detalhes: {Message}", ex.Message);
                return new ResultadoIA
                {
                    Resumo    = "Resumo indisponível",
                    Tags      = "",
                    Categoria = "Geral"
                };
            }
        }
    }
}
