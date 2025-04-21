using ConsultaEmpresa.Domain.Features.Empresas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsultaEmpresa.Infra.Repository.Empresas
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.NomeEmpresarial).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Cnpj).IsRequired().HasMaxLength(14);
            builder.Property(e => e.UsuarioId).IsRequired();
        }
    }
}