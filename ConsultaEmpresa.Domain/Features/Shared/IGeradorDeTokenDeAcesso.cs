namespace ConsultaEmpresa.Domain.Features.Shared;

public interface IGeradorDeTokenDeAcesso
{
    string Gerar(int id);
}