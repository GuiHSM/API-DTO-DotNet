using api.Models;
using MySql.Data.MySqlClient;

namespace api.ModelViews;

public class PessoaFisicaRepositorioMySql
{
    public PessoaFisicaRepositorioMySql()
    {
        conexao = Factory.getConnection();
    }

    private string conexao = "";

    public async Task<List<PessoaFisica>> TodosAsync()
    {
        var lista = new List<PessoaFisica>();
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"select * from pessoasFisicas";

            var command = new MySqlCommand(query, conn);
            var dr = await command.ExecuteReaderAsync();
            while(dr.Read())
            {
                lista.Add(new PessoaFisica{
                    Id = Convert.ToInt32(dr["id"]),
                    Nome = dr["nome"].ToString() ?? "",
                    Telefone = dr["telefone"].ToString() ?? "",
                    CPF = dr["cpf"].ToString() ?? "",
                    DataCriacao = DateTime.Parse(dr["datacriacao"].ToString()),
                });
            }

            conn.Close();
        }

        return lista;
    }

    public async Task IncluirAsync(PessoaFisica pessoaFisica)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"insert into pessoasFisicas(nome,telefone,datacriacao,cpf)values(@nome,@telefone,@datacriacao,@cpf);";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@nome", pessoaFisica.Nome));
            command.Parameters.Add(new MySqlParameter("@telefone", pessoaFisica.Telefone));
            command.Parameters.Add(new MySqlParameter("@datacriacao", pessoaFisica.DataCriacao));
            command.Parameters.Add(new MySqlParameter("@endereco", pessoaFisica.CPF));
            await command.ExecuteNonQueryAsync();

            // caso queira trabalhar com o ID retornado 
            // int id = Convert.ToInt32(command.ExecuteScalar());
            conn.Close();
        }
    }

    public async Task<PessoaFisica> AtualizarAsync(PessoaFisica pessoaFisica)
    {
       using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"update pessoasFisicas set nome=@nome,telefone=@telefone,cpf=@cpf,datacriacao=@datacriacao where id = @id;";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@id", pessoaFisica.Id));
            command.Parameters.Add(new MySqlParameter("@nome", pessoaFisica.Nome));
            command.Parameters.Add(new MySqlParameter("@telefone", pessoaFisica.Telefone));
            command.Parameters.Add(new MySqlParameter("@datacriacao", pessoaFisica.DataCriacao));
            command.Parameters.Add(new MySqlParameter("@cpf", pessoaFisica.CPF));
            await command.ExecuteNonQueryAsync();

            conn.Close();
        }

        return pessoaFisica;
    }

    public async Task ApagarAsync(PessoaFisica pessoaFisica)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"delete from pessoasFisicas where id = @id;";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@id", pessoaFisica.Id));
            await command.ExecuteNonQueryAsync();
            conn.Close();
        }
    }
}