using ConsultaEmpresa.Attributes;
using Microsoft.AspNetCore.Mvc;
using ConsultaEmpresa.Aplication.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ConsultaEmpresa.Controllers
{
    [ApiController]
    [Authentication]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        // POST: api/empresa (cadastrar empresa via CNPJ)
        [HttpPost]
        public async Task<IActionResult> CadastrarEmpresa([FromBody] string cnpj)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            await _empresaService.CadastrarEmpresaAsync(cnpj, userId);
            return Ok("Empresa cadastrada com sucesso.");
        }

        // GET: api/empresa (listar empresas do usuário logado)
        [HttpGet]
        public async Task<IActionResult> ListarEmpresas()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var empresas = await _empresaService.ListarEmpresasDoUsuarioAsync(userId);
            return Ok(empresas);
        }
    }
}