using BlogPessoal.Models;

namespace BlogPessoal.Repositories
{
    public interface IPostagemRepository
    {
        Task<IEnumerable<Postagem>> GetAllAsync();
        Task<Postagem?> GetByIdAsync(long id);
        Task<IEnumerable<Postagem>> GetByTituloAsync(string titulo);
        Task<IEnumerable<Postagem>> GetByTemaAsync(long temaId);
        Task<IEnumerable<Postagem>> GetByUsuarioAsync(long usuarioId);
        Task<Postagem> CreateAsync(Postagem postagem);
        Task<Postagem?> UpdateAsync(Postagem postagem);
        Task<bool> DeleteAsync(long id);
    }

    public interface ITemaRepository
    {
        Task<IEnumerable<Tema>> GetAllAsync();
        Task<Tema?> GetByIdAsync(long id);
        Task<IEnumerable<Tema>> GetByDescricaoAsync(string descricao);
        Task<Tema> CreateAsync(Tema tema);
        Task<Tema?> UpdateAsync(Tema tema);
        Task<bool> DeleteAsync(long id);
    }

    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(long id);
        Task<Usuario?> GetByEmailAsync(string email);
        Task<Usuario> CreateAsync(Usuario usuario);
        Task<Usuario?> UpdateAsync(Usuario usuario);
        Task<bool> DeleteAsync(long id);
    }
}
