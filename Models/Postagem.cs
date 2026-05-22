using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.Models
{
    public class Postagem
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(100, ErrorMessage = "Título deve ter no máximo 100 caracteres")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Texto é obrigatório")]
        public string Texto { get; set; } = string.Empty;

        public DateTime Data { get; set; } = DateTime.UtcNow;

        // Campos gerados pela IA
        public string? ResumoIA { get; set; }
        public string? TagsIA { get; set; }
        public string? CategoriaIA { get; set; }

        // Relacionamentos
        public long? TemaId { get; set; }
        public Tema? Tema { get; set; }

        public long? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
