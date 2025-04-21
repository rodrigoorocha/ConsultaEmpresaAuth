using ConsultaEmpresa.Domain.Features.Empresas;
using ConsultaEmpresa.Aplication.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using ConsultaEmpresa.Infra.Repository.Empresas;

namespace ConsultaEmpresa.Aplication.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly HttpClient _httpClient;

        public EmpresaService(IEmpresaRepository empresaRepository, HttpClient httpClient)
        {
            _empresaRepository = empresaRepository;
            _httpClient = httpClient;
        }

        public async Task CadastrarEmpresaAsync(string cnpj, string userId)
        {
            var response = await _httpClient.GetAsync($"https://www.receitaws.com.br/v1/cnpj/{cnpj}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var empresaData = JsonSerializer.Deserialize<Empresa>(json);

            if (empresaData != null)
            {
                empresaData.UsuarioId = int.Parse(userId);
                await _empresaRepository.CriaAsync(empresaData);
            }
        }

        public async Task<IEnumerable<Empresa>> ListarEmpresasDoUsuarioAsync(string userId)
        {
            return await _empresaRepository.GetByUserIdAsync(int.Parse(userId));
        }
    }
}