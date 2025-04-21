using ConsultaEmpresa.Domain.Features.Usuarios;
using ConsultaEmpresa.Aplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaEmpresa.Web.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GeradorDeTokenController : ControllerBase
{
    private readonly GeradorDeTokenService _geradorDeTokenService;

    public GeradorDeTokenController(GeradorDeTokenService geradorDeTokenService)
    {
        _geradorDeTokenService = geradorDeTokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] Login login)
    {
        try
        {
            var token = _geradorDeTokenService.Autenticar(login);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Mensagem = ex.Message });
        }
    }
}