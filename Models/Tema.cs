using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogPessoal.Models
{
    public class Tema
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(255)]
        public string Descricao { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Postagem>? Postagens { get; set; }
    }
}
