using ConsultaEmpresa.Domain.Dto;
using ConsultaEmpresa.Domain.Features.Shared.Exceptions;
using ConsultaEmpresa.Domain.Features.Usuarios;
using ConsultaEmpresa.Infra;
using Microsoft.Extensions.Logging;

namespace ConsultaEmpresa.Aplication.Services;

public class UsuarioService : IUsuarioService
{
    public IUsuarioRepository _usuarioRepository;
    private ILogger<UsuarioService> _logger;
    private SenhaEncriptador _senhaEncriptador;

    public UsuarioService(IUsuarioRepository usuarioRepository, ILogger<UsuarioService> logger, SenhaEncriptador senhaEncriptador)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
        _senhaEncriptador = senhaEncriptador;
    }

    public async Task<string> CriarUsuarioAsync(UsuarioDto usuario)
    {
        var usuarioJaExiste = await _usuarioRepository.ExistePorEmailAsync(usuario.Email);
        if (usuarioJaExiste)
        {
            var exception = new AlreadyExistsException($"Já existe usuário com email: {usuario.Email}");
            _logger.LogError(exception.Message);
            throw exception;
        }

        var senhaEncriptada = _senhaEncriptador.Encriptar(usuario.Senha);

        var novoUsuario = new Usuario(usuario.Email, senhaEncriptada);

        await _usuarioRepository.CriaAsync(novoUsuario);

        return usuario.Email;
    }
}