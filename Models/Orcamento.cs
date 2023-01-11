namespace api.Models;

public record Orcamento
{
    public int Id { get;set; } = default!;
    public int? ClienteId { get;set; }
    public int? FornecedorId { get;set; }
    public string DescricaoDoServico { get;set; } = default!;
    public double ValorTotal { get;set; } = default!;
    public DateTime DataCriacao { get;set; } = default!;
    public DateTime DataExpiracao { get;set; } = default!;
}