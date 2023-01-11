using System.Text.Json.Serialization;

namespace api.DTOs;

public record PessoaJuridicaDTO
{
    [JsonPropertyName("name")]
    public string NNPJ { get;set; } = default!;
    [JsonPropertyName("cpf")]
    public string CNPJ { get;set; } = default!;

    [JsonPropertyName("phone")]
    public string? Telefone { get;set; }

    [JsonPropertyName("creation_date")]
    public DateTime DataCricao { get;set; } = default!;
}