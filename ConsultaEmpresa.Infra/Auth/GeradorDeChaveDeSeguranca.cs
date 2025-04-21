using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ConsultaEmpresa.Infra.Auth;

public abstract class GeradorDeChaveDeSeguranca
{
    protected SymmetricSecurityKey GerarSecurityKey(string chaveDeAssinatura)
    {
        var bytes = Encoding.UTF8.GetBytes(chaveDeAssinatura);

        return new SymmetricSecurityKey(bytes);
    }
}