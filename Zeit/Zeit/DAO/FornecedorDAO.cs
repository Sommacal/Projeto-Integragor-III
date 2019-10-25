using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace Zeit
{
    public class FornecedorDAO
    {
        public void inserir(Fornecedor fornecedor)
        {
            Conexao connection = new Conexao();
            NpgsqlCommand query = new NpgsqlCommand("INSERT INTO fornecedor (nome, cnpj, email, telefone, endereco) values (@nome, @cnpj, @email, @telefone, @endereco)");
            query.Connection = connection.Open();
            query.Parameters.Add("nome", NpgsqlTypes.NpgsqlDbType.Varchar).Value = fornecedor.nome;
            query.Parameters.Add("cnpj", NpgsqlTypes.NpgsqlDbType.Varchar).Value = fornecedor.cnpj;
            query.Parameters.Add("email", NpgsqlTypes.NpgsqlDbType.Varchar).Value = fornecedor.email;
            query.Parameters.Add("telefone", NpgsqlTypes.NpgsqlDbType.Varchar).Value = fornecedor.telefone;
            query.Parameters.Add("endereco", NpgsqlTypes.NpgsqlDbType.Varchar).Value = fornecedor.endereco;
            query.ExecuteNonQuery();
            connection.Close();
        }

        public List<Fornecedor> listafornecedores()
        {
            Conexao connection = new Conexao();
            NpgsqlCommand query = new NpgsqlCommand("select id, nome, cnpj, email, telefone, endereco from fornecedor");
            query.Connection = connection.Open();
            using (NpgsqlDataReader rs = query.ExecuteReader())
                if (rs.HasRows)
                {
                    List<Fornecedor> fornecedores = new List<Fornecedor>();
                    while (rs.Read())
                    {
                        Fornecedor f = new Fornecedor();
                        f.id = rs.GetInt32(0);
                        f.nome = rs.GetString(1);
                        f.cnpj = rs.GetString(2);
                        f.email = rs.GetString(3);
                        f.telefone = rs.GetString(4);
                        f.endereco = rs.GetString(5);
                        fornecedores.Add(f);
                    }
                    connection.Close();
                    return fornecedores;
                }
            connection.Close();
            return null;
        }
    }
}
