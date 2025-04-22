using ConsultaEmpresa.Attributes;
using Microsoft.AspNetCore.Mvc;
using ConsultaEmpresa.Aplication.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ConsultaEmpresa.Domain.Features.Shared;
using ConsultaEmpresa.Domain.Dto;

namespace ConsultaEmpresa.Controllers
{
    [ApiController]
    [Authentication]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;
        private readonly IValidadorToken _validadorToken;

        public EmpresaController(IEmpresaService empresaService, IValidadorToken validadorToken)
        {
            _empresaService = empresaService;
            _validadorToken = validadorToken;
        }

        // POST: api/empresa (cadastrar empresa via CNPJ)
        [HttpPost]
        public async Task<IActionResult> CadastrarEmpresa([FromBody] EmpresaDto empresaDto)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();

            var idDoUsuario = _validadorToken.Validar(token);

            await _empresaService.CadastrarEmpresaAsync(empresaDto, idDoUsuario);
            return Ok("Empresa cadastrada com sucesso.");
        }

        // GET: api/empresa (listar empresas do usuário logado)
        [HttpGet]
        public async Task<IActionResult> ListarEmpresasDoUsuario()
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var userId = _validadorToken.Validar(token).ToString();

            var empresas = await _empresaService.ListarEmpresasDoUsuarioAsync(userId);
            return Ok(empresas);
        }
    }
}