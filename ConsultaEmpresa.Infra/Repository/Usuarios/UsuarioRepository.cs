using ConsultaEmpresa.Domain.Features.Usuarios;
using ConsultaEmpresa.Infra.Context;
using ConsultaEmpresa.Infra.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace ConsultaEmpresa.Infra.Repository.Usuarios;

public class UsuarioRepository : IUsuarioRepository
{
    private AppDbContext _dbContext;

    public UsuarioRepository(AppDbContext context)
    {
        _dbContext = context;
    }

    public async Task CriaAsync(Usuario usuario)
    {
        await _dbContext.Usuarios.AddAsync(usuario);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistePorEmailAsync(string email)
    {
        return await _dbContext.Usuarios.AnyAsync(b => b.Email == email);
    }

    public async Task<bool> ExistePorIdAsync(int id)
    {
        return await _dbContext.Usuarios.AnyAsync(b => b.Id == id);
    }

    public async Task<int> ExisteLoginAsync(string email, string senha)
    {
        return await _dbContext.Usuarios.Where(u => u.Email == email && u.SenhaHash == senha)
            .Select(u => u.Id)
            .FirstOrDefaultAsync();
    }
}