using System.Text.Json.Serialization;

namespace api.DTOs;

public record PessoaFisicaDTO
{
    [JsonPropertyName("name")]
    public string Nome { get;set; } = default!;
    [JsonPropertyName("cpf")]
    public string CPF { get;set; } = default!;

    [JsonPropertyName("phone")]
    public string? Telefone { get;set; }

    [JsonPropertyName("creation_date")]
    public DateTime DataCricao { get;set; } = default!;
}