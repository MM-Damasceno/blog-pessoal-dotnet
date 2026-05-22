namespace BlogPessoal.Services.IA
{
    public static class PromptBuilder
    {
        public static string BuildResumoPrompt(string texto)
        {
            return "Analise o texto abaixo e retorne SOMENTE um JSON válido (sem markdown) no formato:\n" +
                   "{\n" +
                   "  \"resumo\": \"resumo em até 2 frases\",\n" +
                   "  \"tags\": \"tag1, tag2, tag3\",\n" +
                   "  \"categoria\": \"nome da categoria\"\n" +
                   "}\n\n" +
                   "Texto: " + texto;
        }
    }
}