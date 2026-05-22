using BlogPessoal.Data;
using BlogPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync() =>
            await _context.Usuarios.ToListAsync();

        public async Task<Usuario?> GetByIdAsync(long id) =>
            await _context.Usuarios.FindAsync(id);

        public async Task<Usuario?> GetByEmailAsync(string email) =>
            await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario?> UpdateAsync(Usuario usuario)
        {
            var existing = await _context.Usuarios.FindAsync(usuario.Id);
            if (existing == null) return null;

            existing.Nome  = usuario.Nome;
            existing.Email = usuario.Email;
            existing.Senha = usuario.Senha;
            existing.Foto  = usuario.Foto;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
