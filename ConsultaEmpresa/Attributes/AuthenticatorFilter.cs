using ConsultaEmpresa.Domain.Features.Shared;
using ConsultaEmpresa.Domain.Features.Shared.Exceptions;
using ConsultaEmpresa.Domain.Features.Usuarios;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ConsultaEmpresa.Attributes
{
    public class AuthenticatorFilter : IAsyncAuthorizationFilter
    {
        private readonly IValidadorToken _validadorToken;
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthenticatorFilter(IValidadorToken validadorToken, IUsuarioRepository usuarioRepository)
        {
            _validadorToken = validadorToken;
            _usuarioRepository = usuarioRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var token = TokenOnRequest(context);

            var idDoUsuario = _validadorToken.Validar(token);

            var existeUsuario = await _usuarioRepository.ExistePorIdAsync(idDoUsuario);
            if (existeUsuario is false)
            {
                throw new InvalidLogin("Autenticação inválida!");
            }
        }

        private string TokenOnRequest(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers.Authorization.ToString();
            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidLogin("Autenticação não informada!");
            }

            return token["Bearer ".Length..].Trim();
        }
    }
}