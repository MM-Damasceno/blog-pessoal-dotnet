using BlogPessoal.DTOs;
using BlogPessoal.Models;

namespace BlogPessoal.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO?> CadastrarAsync(UsuarioCadastroDTO dto);
        Task<UsuarioDTO?> AtualizarAsync(long id, UsuarioCadastroDTO dto);
        Task<bool> DeletarAsync(long id);
        Task<string?> LoginAsync(UsuarioLogin login);
    }
}
