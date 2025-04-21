using ConsultaEmpresa.Domain.Dto;
using ConsultaEmpresa.Domain.Features.Usuarios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsultaEmpresa.Aplication.Services
{
    public interface IUsuarioService
    {
        Task<string> CriarUsuarioAsync(UsuarioDto usuario);
    }
}