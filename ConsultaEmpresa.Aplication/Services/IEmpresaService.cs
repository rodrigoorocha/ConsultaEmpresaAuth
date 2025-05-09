using ConsultaEmpresa.Domain.Dto;
using ConsultaEmpresa.Domain.Features.Empresas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsultaEmpresa.Aplication.Services
{
    public interface IEmpresaService
    {
        Task CadastrarEmpresaAsync(EmpresaDto empresaDto, int userId);

        Task<IEnumerable<Empresa>> ListarEmpresasDoUsuarioAsync(string userId);
    }
}