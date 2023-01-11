using System.Text.Json.Serialization;

namespace api.DTOs;

public record OrcamentoDTO
{
    [JsonPropertyName("name")]
    public string Nome { get;set; } = default!;
    [JsonPropertyName("clientId")]
    public int? ClienteId { get;set; }

    [JsonPropertyName("clientId")]
    public int? FornecedorId { get;set; }

    [JsonPropertyName("phone")]
    public string? Telefone { get;set; }

    [JsonPropertyName("creationdate")]
    public DateTime DataCricao { get;set; } = default!;

    [JsonPropertyName("expirationdate")]
    public DateTime DataExpiracao { get;set; } = default!;
    
    [JsonPropertyName("description")]
    public string DescricaoDoServico { get;set; } = default!;
    
    [JsonPropertyName("totalvalue")]
    public double ValorTotal { get;set; } = default!;
}