using Microsoft.AspNetCore.Mvc;
using ConsultaEmpresa.Domain.Features.Usuarios;
using AutoMapper;
using ConsultaEmpresa.Aplication.Services;
using ConsultaEmpresa.Domain.Features.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;
using ConsultaEmpresa.Domain.Dto;

namespace ConsultaEmpresa.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AlreadyExistsException), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Cria([FromBody] UsuarioDto usuarioDTO)
    {
        var email = await _usuarioService.CriarUsuarioAsync(usuarioDTO);

        return Created(string.Empty, email);
    }
}