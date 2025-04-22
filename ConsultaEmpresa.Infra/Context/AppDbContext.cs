using Microsoft.EntityFrameworkCore;
using ConsultaEmpresa.Domain.Features.Usuarios;
using ConsultaEmpresa.Domain.Features.Empresas;
using ConsultaEmpresa.Infra.Repository;
using ConsultaEmpresa.Infra.Repository.Usuarios;
using ConsultaEmpresa.Infra.Repository.Empresas;

namespace ConsultaEmpresa.Infra.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }

        // Add DbSet properties for your entities here
        // public DbSet<YourEntity> YourEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new EmpresaConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}