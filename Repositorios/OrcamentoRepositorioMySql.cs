using api.Models;
using MySql.Data.MySqlClient;

namespace api.ModelViews;

public class OrcamentoRepositorioMySql
{
    public OrcamentoRepositorioMySql()
    {
        conexao = Factory.getConnection();
    }

    private string conexao = "";

    public async Task<List<Orcamento>> TodosAsync()
    {
        var lista = new List<Orcamento>();
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"select * from orcamentos";

            var command = new MySqlCommand(query, conn);
            var dr = await command.ExecuteReaderAsync();
            while(dr.Read())
            {
                lista.Add(new Orcamento{
                    Id = Convert.ToInt32(dr["id"]),
                    ClienteId = Convert.ToInt32(dr["clienteid"]),
                    FornecedorId = Convert.ToInt32(dr["fornecedorid"]),
                    DescricaoDoServico = dr["descricaodoservico"].ToString() ?? "",
                    DataCriacao = DateTime.Parse(dr["datacriacao"].ToString()),
                    ValorTotal = Convert.ToInt32(dr["valortotal"]),
                    DataExpiracao = DateTime.Parse(dr["dataexpiracao"].ToString()),
                });
            }

            conn.Close();
        }

        return lista;
    }

    public async Task IncluirAsync(Orcamento orcamento)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"insert into orcamentos(clienteid,fornecedorid,datacriacao,descricaodoservico,valortotal,dataexpiracao)values(@clienteid,@fornecedorid,@datacriacao,@descricaodoservico,@valortotal,@dataexpiracao);";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@id", orcamento.Id));
            command.Parameters.Add(new MySqlParameter("@clienteid", orcamento.ClienteId));
            command.Parameters.Add(new MySqlParameter("@fornecedorid", orcamento.FornecedorId));
            command.Parameters.Add(new MySqlParameter("@datacriacao", orcamento.DataCriacao));
            command.Parameters.Add(new MySqlParameter("@descricaodoservico", orcamento.DescricaoDoServico));
            command.Parameters.Add(new MySqlParameter("@valortotal", orcamento.ValorTotal));
            command.Parameters.Add(new MySqlParameter("@dataexpiracao", orcamento.DataExpiracao));
            await command.ExecuteNonQueryAsync();

            // caso queira trabalhar com o ID retornado 
            // int id = Convert.ToInt32(command.ExecuteScalar());
            conn.Close();
        }
    }

    public async Task<Orcamento> AtualizarAsync(Orcamento orcamento)
    {
       using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"update orcamentos set clienteid=@clienteid,fornecedorid=@fornecedorid,descricaodoservico=@descricaodoservico,valortotal=@valortotal,datacriacao=@datacriacao,dataexpiracao=@dataexpiracao where id = @id;";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@id", orcamento.Id));
            command.Parameters.Add(new MySqlParameter("@clienteid", orcamento.ClienteId));
            command.Parameters.Add(new MySqlParameter("@fornecedorid", orcamento.FornecedorId));
            command.Parameters.Add(new MySqlParameter("@datacriacao", orcamento.DataCriacao));
            command.Parameters.Add(new MySqlParameter("@descricaodoservico", orcamento.DescricaoDoServico));
            command.Parameters.Add(new MySqlParameter("@valortotal", orcamento.ValorTotal));
            command.Parameters.Add(new MySqlParameter("@dataexpiracao", orcamento.DataExpiracao));
            await command.ExecuteNonQueryAsync();

            conn.Close();
        }

        return orcamento;
    }

    public async Task ApagarAsync(Orcamento orcamento)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"delete from orcamentos where id = @id;";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@id", orcamento.Id));
            await command.ExecuteNonQueryAsync();
            conn.Close();
        }
    }
}