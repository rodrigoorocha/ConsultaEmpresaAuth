using Microsoft.AspNetCore.Mvc;
using ConsultaEmpresa.Aplication.Services;
using ConsultaEmpresa.Domain.Features.Usuarios;
using ConsultaEmpresa.Domain.Dto;

namespace ConsultaEmpresa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly GeradorDeTokenService _geradorDeTokenService;

        public AuthController(GeradorDeTokenService geradorDeTokenService)
        {
            _geradorDeTokenService = geradorDeTokenService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            var token = await _geradorDeTokenService.GerarTokenDeAcesso(usuarioLoginDto);

            return Created(string.Empty, token);
        }
    }
}