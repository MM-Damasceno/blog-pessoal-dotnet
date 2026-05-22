using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs
{
    // Retornado nas respostas (não mostra senha)
    public class UsuarioDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Foto { get; set; }
    }

    // Recebido no cadastro e atualização
    public class UsuarioCadastroDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(8, ErrorMessage = "Senha deve ter no mínimo 8 caracteres")]
        public string Senha { get; set; } = string.Empty;

        public string? Foto { get; set; }
    }

    // Retornado pelo endpoint de IA
    public class ResultadoIA
    {
        public string Resumo { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
    }
}
