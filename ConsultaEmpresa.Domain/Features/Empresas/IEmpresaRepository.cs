namespace ConsultaEmpresa.Domain.Features.Empresas
{
    public interface IEmpresaRepository
    {
        Empresa GetByCnpj(string cnpj);

        Task CriaAsync(Empresa empresa);

        Task<IEnumerable<Empresa>> GetByUserIdAsync(int userId);
    }
}