using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ConsultaEmpresa.Domain.Features.Empresas;

public class Empresa
{
    public int Id { get; set; }

    [JsonPropertyName("nome")]
    public string NomeEmpresarial { get; set; }

    [JsonPropertyName("fantasia")]
    public string NomeFantasia { get; set; }

    [JsonPropertyName("cnpj")]
    public string Cnpj { get; set; }

    [JsonPropertyName("situacao")]
    public string Situacao { get; set; }

    [JsonPropertyName("abertura")]
    public string Abertura { get; set; }

    [JsonPropertyName("tipo")]
    public string Tipo { get; set; }

    [JsonPropertyName("natureza_juridica")]
    public string NaturezaJuridica { get; set; }

    [JsonPropertyName("logradouro")]
    public string Logradouro { get; set; }

    [JsonPropertyName("numero")]
    public string Numero { get; set; }

    [JsonPropertyName("complemento")]
    public string Complemento { get; set; }

    [JsonPropertyName("bairro")]
    public string Bairro { get; set; }

    [JsonPropertyName("municipio")]
    public string Municipio { get; set; }

    [JsonPropertyName("uf")]
    public string Uf { get; set; }

    [JsonPropertyName("cep")]
    public string Cep { get; set; }

    public int UsuarioId { get; set; }
}