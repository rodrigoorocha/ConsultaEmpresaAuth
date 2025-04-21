using ConsultaEmpresa.Domain.Features.Shared.Exceptions;
using ConsultaEmpresa.Domain.Features.Shared;
using ConsultaEmpresa.Domain.Features.Usuarios;
using ConsultaEmpresa.Infra.Repository.Usuarios;
using Microsoft.Extensions.Logging;
using ConsultaEmpresa.Infra;
using ConsultaEmpresa.Domain.Dto;

namespace ConsultaEmpresa.Aplication.Services;

public class GeradorDeTokenService
{
    private readonly SenhaEncriptador _senhaEncriptador;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IGeradorDeTokenDeAcesso _geradorJwtToken;
    private readonly ILogger<GeradorDeTokenService> _logger;

    public GeradorDeTokenService(SenhaEncriptador senhaEncriptador, IUsuarioRepository usuarioRepository, IGeradorDeTokenDeAcesso geradorJwtToken, ILogger<GeradorDeTokenService> logger)
    {
        _senhaEncriptador = senhaEncriptador;
        _usuarioRepository = usuarioRepository;
        _geradorJwtToken = geradorJwtToken;
        _logger = logger;
    }

    public async Task<string> GerarTokenDeAcesso(UsuarioLoginDto login)
    {
        var senhaEncriptada = _senhaEncriptador.Encriptar(login.Senha);

        var idDoUsuario = await _usuarioRepository.ExisteLoginAsync(login.Email, senhaEncriptada);

        if (idDoUsuario == 0)
        {
            var exception = new InvalidLogin($"Senha ou email incorreto!");
            _logger.LogError(exception.Message);
            throw exception;
        }

        return _geradorJwtToken.Gerar(idDoUsuario);
    }
}