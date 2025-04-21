using Microsoft.AspNetCore.Mvc;

namespace ConsultaEmpresa.Attributes
{
    public class AuthenticationAttribute : TypeFilterAttribute
    {
        public AuthenticationAttribute() : base(typeof(AuthenticatorFilter))
        {
        }
    }
}