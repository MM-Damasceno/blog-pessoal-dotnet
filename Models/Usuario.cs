using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogPessoal.Models
{
    public class Usuario
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(8, ErrorMessage = "Senha deve ter no mínimo 8 caracteres")]
        public string Senha { get; set; } = string.Empty;

        public string? Foto { get; set; }

        [JsonIgnore]
        public ICollection<Postagem>? Postagens { get; set; }
    }
}
