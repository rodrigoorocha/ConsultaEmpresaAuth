using ConsultaEmpresa.Domain.Features.Empresas;
using ConsultaEmpresa.Domain.Features.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsultaEmpresa.Infra.Repository.Empresas
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            // Define table name
            builder.ToTable("Empresas");

            // Define primary key
            builder.HasKey(e => e.Id);

            // Configure properties with increased max lengths
            builder.Property(e => e.NomeEmpresarial)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.NomeFantasia)
                .HasMaxLength(200);

            builder.Property(e => e.Cnpj)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Situacao)
                .HasMaxLength(100);

            builder.Property(e => e.Abertura)
                .HasMaxLength(10);

            builder.Property(e => e.Tipo)
                .HasMaxLength(50);

            builder.Property(e => e.NaturezaJuridica)
                .HasMaxLength(100);

            builder.Property(e => e.Logradouro)
                .HasMaxLength(200);

            builder.Property(e => e.Numero)
                .HasMaxLength(20);

            builder.Property(e => e.Complemento)
                .HasMaxLength(100);

            builder.Property(e => e.Bairro)
                .HasMaxLength(100);

            builder.Property(e => e.Municipio)
                .HasMaxLength(100);

            builder.Property(e => e.Uf)
                .HasMaxLength(20);

            builder.Property(e => e.Cep)
                .HasMaxLength(10);

            // Configure relationships
            builder.HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}