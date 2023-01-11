using api.Models;
using MySql.Data.MySqlClient;

namespace api.ModelViews;

public class PessoaJuridicaRepositorioMySql
{
    public PessoaJuridicaRepositorioMySql()
    {
        conexao = Factory.getConnection();
    }

    private string conexao = "";

    public async Task<List<PessoaJuridica>> TodosAsync()
    {
        var lista = new List<PessoaJuridica>();
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"select * from pessoasJuridicas";

            var command = new MySqlCommand(query, conn);
            var dr = await command.ExecuteReaderAsync();
            while(dr.Read())
            {
                lista.Add(new PessoaJuridica{
                    Id = Convert.ToInt32(dr["id"]),
                    Nome = dr["nome"].ToString() ?? "",
                    Telefone = dr["telefone"].ToString() ?? "",
                    CNPJ = dr["cnpj"].ToString() ?? "",
                    DataCriacao = DateTime.Parse(dr["datacriacao"].ToString()),
                });
            }

            conn.Close();
        }

        return lista;
    }

    public async Task IncluirAsync(PessoaJuridica pessoaJuridica)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"insert into pessoasJuridicas(nome,telefone,datacriacao,cnpj)values(@nome,@telefone,@datacriacao,@cnpj);";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@nome", pessoaJuridica.Nome));
            command.Parameters.Add(new MySqlParameter("@telefone", pessoaJuridica.Telefone));
            command.Parameters.Add(new MySqlParameter("@datacriacao", pessoaJuridica.DataCriacao));
            command.Parameters.Add(new MySqlParameter("@cnpj", pessoaJuridica.CNPJ));
            await command.ExecuteNonQueryAsync();

            // caso queira trabalhar com o ID retornado 
            // int id = Convert.ToInt32(command.ExecuteScalar());
            conn.Close();
        }
    }

    public async Task<PessoaJuridica> AtualizarAsync(PessoaJuridica pessoaJuridica)
    {
       using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"update pessoasJuridicas set nome=@nome,telefone=@telefone,datacriacao=@datacriacao,cnpj=@cnpj where id = @id;";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@id", pessoaJuridica.Id));
            command.Parameters.Add(new MySqlParameter("@nome", pessoaJuridica.Nome));
            command.Parameters.Add(new MySqlParameter("@telefone", pessoaJuridica.Telefone));
            command.Parameters.Add(new MySqlParameter("@datacriacao", pessoaJuridica.DataCriacao));
            command.Parameters.Add(new MySqlParameter("@cnpj", pessoaJuridica.CNPJ));
            await command.ExecuteNonQueryAsync();

            conn.Close();
        }

        return pessoaJuridica;
    }

    public async Task ApagarAsync(PessoaJuridica pessoaJuridica)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"delete from pessoasJuridicas where id = @id;";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@id", pessoaJuridica.Id));
            await command.ExecuteNonQueryAsync();
            conn.Close();
        }
    }
}