using ConsultaEmpresa.Domain.Features.Empresas;
using ConsultaEmpresa.Aplication.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using ConsultaEmpresa.Infra.Repository.Empresas;
using ConsultaEmpresa.Domain.Dto;
using System;

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

        public async Task CadastrarEmpresaAsync(EmpresaDto empresaDto, int userId)
        {
            var response = await _httpClient.GetAsync($"https://www.receitaws.com.br/v1/cnpj/{empresaDto.cnpj}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            // Use custom JsonSerializerOptions to handle case-insensitive deserialization
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var empresaData = JsonSerializer.Deserialize<Empresa>(json, options);

            // Log or debug the raw JSON response for troubleshooting
            if (empresaData == null)
            {
                throw new Exception($"Failed to deserialize Empresa. Raw JSON: {json}");
            }

            empresaData.UsuarioId = userId;
            await _empresaRepository.CriaAsync(empresaData);
        }

        public async Task<IEnumerable<Empresa>> ListarEmpresasDoUsuarioAsync(string userId)
        {
            return await _empresaRepository.GetByUserIdAsync(int.Parse(userId));
        }
    }
}