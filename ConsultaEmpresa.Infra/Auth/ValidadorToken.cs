using ConsultaEmpresa.Domain.Features.Shared;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaEmpresa.Infra.Auth;

public class ValidadorToken : GeradorDeChaveDeSeguranca, IValidadorToken
{
    private readonly string _chaveDeAssinatura;

    public ValidadorToken(string chaveDeAssinatura)
    {
        _chaveDeAssinatura = chaveDeAssinatura;
    }

    public int Validar(string token)
    {
        // Remove the 'Bearer ' prefix if it exists
        if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            token = token.Substring("Bearer ".Length).Trim();
        }

        var validacaoDeParametros = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = GerarSecurityKey(_chaveDeAssinatura),
            ClockSkew = new TimeSpan(0)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principalClaim = tokenHandler.ValidateToken(token, validacaoDeParametros, out _);

        var idDoUsuario = principalClaim.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        return int.Parse(idDoUsuario);
    }
}