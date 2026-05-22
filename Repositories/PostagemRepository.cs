using BlogPessoal.Data;
using BlogPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Repositories
{
    public class PostagemRepository : IPostagemRepository
    {
        private readonly AppDbContext _context;

        public PostagemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Postagem>> GetAllAsync() =>
            await _context.Postagens
                .Include(p => p.Tema)
                .Include(p => p.Usuario)
                .OrderByDescending(p => p.Data)
                .ToListAsync();

        public async Task<Postagem?> GetByIdAsync(long id) =>
            await _context.Postagens
                .Include(p => p.Tema)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Postagem>> GetByTituloAsync(string titulo) =>
            await _context.Postagens
                .Include(p => p.Tema)
                .Include(p => p.Usuario)
                .Where(p => p.Titulo.Contains(titulo))
                .ToListAsync();

        public async Task<IEnumerable<Postagem>> GetByTemaAsync(long temaId) =>
            await _context.Postagens
                .Include(p => p.Tema)
                .Include(p => p.Usuario)
                .Where(p => p.TemaId == temaId)
                .ToListAsync();

        public async Task<IEnumerable<Postagem>> GetByUsuarioAsync(long usuarioId) =>
            await _context.Postagens
                .Include(p => p.Tema)
                .Include(p => p.Usuario)
                .Where(p => p.UsuarioId == usuarioId)
                .ToListAsync();

        public async Task<Postagem> CreateAsync(Postagem postagem)
        {
            _context.Postagens.Add(postagem);
            await _context.SaveChangesAsync();
            return postagem;
        }

        public async Task<Postagem?> UpdateAsync(Postagem postagem)
        {
            var existing = await _context.Postagens.FindAsync(postagem.Id);
            if (existing == null) return null;

            existing.Titulo      = postagem.Titulo;
            existing.Texto       = postagem.Texto;
            existing.TemaId      = postagem.TemaId;
            existing.ResumoIA    = postagem.ResumoIA;
            existing.TagsIA      = postagem.TagsIA;
            existing.CategoriaIA = postagem.CategoriaIA;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var postagem = await _context.Postagens.FindAsync(id);
            if (postagem == null) return false;

            _context.Postagens.Remove(postagem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
