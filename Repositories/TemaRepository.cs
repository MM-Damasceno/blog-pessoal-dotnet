using BlogPessoal.Data;
using BlogPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Repositories
{
    public class TemaRepository : ITemaRepository
    {
        private readonly AppDbContext _context;

        public TemaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tema>> GetAllAsync() =>
            await _context.Temas.ToListAsync();

        public async Task<Tema?> GetByIdAsync(long id) =>
            await _context.Temas.FindAsync(id);

        public async Task<IEnumerable<Tema>> GetByDescricaoAsync(string descricao) =>
            await _context.Temas
                .Where(t => t.Descricao.Contains(descricao))
                .ToListAsync();

        public async Task<Tema> CreateAsync(Tema tema)
        {
            _context.Temas.Add(tema);
            await _context.SaveChangesAsync();
            return tema;
        }

        public async Task<Tema?> UpdateAsync(Tema tema)
        {
            var existing = await _context.Temas.FindAsync(tema.Id);
            if (existing == null) return null;

            existing.Descricao = tema.Descricao;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var tema = await _context.Temas.FindAsync(id);
            if (tema == null) return false;

            _context.Temas.Remove(tema);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
