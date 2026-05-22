using BlogPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Postagem> Postagens { get; set; }
        public DbSet<Tema> Temas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Postagem -> Usuario (1:N)
            modelBuilder.Entity<Postagem>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Postagens)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Postagem -> Tema (1:N)
            modelBuilder.Entity<Postagem>()
                .HasOne(p => p.Tema)
                .WithMany(t => t.Postagens)
                .HasForeignKey(p => p.TemaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Email único
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
