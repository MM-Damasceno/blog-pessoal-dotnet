using BlogPessoal.Config;
using BlogPessoal.DTOs;
using BlogPessoal.Models;
using BlogPessoal.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BlogPessoal.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository   _repo;
        private readonly IPasswordHasher<Usuario> _hasher;
        private readonly JwtService           _jwtService;

        public UsuarioService(
            IUsuarioRepository repo,
            IPasswordHasher<Usuario> hasher,
            JwtService jwtService)
        {
            _repo       = repo;
            _hasher     = hasher;
            _jwtService = jwtService;
        }

        public async Task<UsuarioDTO?> CadastrarAsync(UsuarioCadastroDTO dto)
        {
            // Verificar email duplicado
            if (await _repo.GetByEmailAsync(dto.Email) != null)
                return null;

            var usuario = new Usuario
            {
                Nome  = dto.Nome,
                Email = dto.Email,
                Foto  = dto.Foto
            };

            // Hash da senha antes de salvar
            usuario.Senha = _hasher.HashPassword(usuario, dto.Senha);

            var criado = await _repo.CreateAsync(usuario);
            return MapToDTO(criado);
        }

        public async Task<string?> LoginAsync(UsuarioLogin login)
        {
            var usuario = await _repo.GetByEmailAsync(login.Email);
            if (usuario == null) return null;

            var resultado = _hasher.VerifyHashedPassword(
                usuario, usuario.Senha, login.Senha);

            if (resultado == PasswordVerificationResult.Failed)
                return null;

            return _jwtService.GerarToken(usuario);
        }

        public async Task<UsuarioDTO?> AtualizarAsync(long id, UsuarioCadastroDTO dto)
        {
            var usuario = await _repo.GetByIdAsync(id);
            if (usuario == null) return null;

            // Verificar conflito de email com outro usuário
            if (usuario.Email != dto.Email)
            {
                if (await _repo.GetByEmailAsync(dto.Email) != null)
                    return null;
                usuario.Email = dto.Email;
            }

            usuario.Nome  = dto.Nome;
            usuario.Foto  = dto.Foto;
            usuario.Senha = _hasher.HashPassword(usuario, dto.Senha);

            var atualizado = await _repo.UpdateAsync(usuario);
            return atualizado == null ? null : MapToDTO(atualizado);
        }

        public async Task<bool> DeletarAsync(long id) =>
            await _repo.DeleteAsync(id);

        private static UsuarioDTO MapToDTO(Usuario u) => new()
        {
            Id    = u.Id.GetValueOrDefault(),
            Nome  = u.Nome,
            Email = u.Email,
            Foto  = u.Foto
        };
    }
}
