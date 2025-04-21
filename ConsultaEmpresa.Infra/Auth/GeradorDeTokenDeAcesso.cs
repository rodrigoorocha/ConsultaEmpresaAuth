using ConsultaEmpresa.Domain.Features.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConsultaEmpresa.Infra.Auth;

public class GeradorDeTokenDeAcesso : GeradorDeChaveDeSeguranca, IGeradorDeTokenDeAcesso
{
    private readonly uint _minutosDeDuracao;
    private readonly string _chaveDeAssinatura;

    public GeradorDeTokenDeAcesso(uint minutosDeDuracao, string chaveDeAssinatura)
    {
        _minutosDeDuracao = minutosDeDuracao;
        _chaveDeAssinatura = chaveDeAssinatura;
    }

    public string Gerar(int id)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Sid, id.ToString())
        };

        var securityKey = GerarSecurityKey(_chaveDeAssinatura);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_minutosDeDuracao),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var secutiryToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(secutiryToken);
    }
}