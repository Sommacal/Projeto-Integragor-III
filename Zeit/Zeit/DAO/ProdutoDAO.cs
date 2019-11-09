using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace Zeit
{
    public class ProdutoDAO
    {
        public void inserir(Produto produto)
        {
            Conexao connection = new Conexao();
            NpgsqlCommand query = new NpgsqlCommand("INSERT INTO produto (nome, descricao, quantidade, validade, id_fornecedor, id_departamento) values (@nome, @descricao, @quantidade, @validade, @id_fornecedor, @id_departamento)");
            query.Connection = connection.Open();
            query.Parameters.Add("nome", NpgsqlTypes.NpgsqlDbType.Varchar).Value = produto.nome;
            query.Parameters.Add("descricao", NpgsqlTypes.NpgsqlDbType.Varchar).Value = produto.descricao;
            query.Parameters.Add("quantidade", NpgsqlTypes.NpgsqlDbType.Integer).Value = produto.quantidade;
            query.Parameters.Add("id_fornecedor", NpgsqlTypes.NpgsqlDbType.Integer).Value = produto.id_fornecedor;
            query.Parameters.Add("id_departamento", NpgsqlTypes.NpgsqlDbType.Integer).Value = produto.id_departamento;
            if (produto.validade is null)
            {
                query.Parameters.Add("validade", NpgsqlTypes.NpgsqlDbType.Date).Value = DBNull.Value;
            }
            else
            {
                query.Parameters.Add("validade", NpgsqlTypes.NpgsqlDbType.Date).Value = produto.validade;
            }
            query.ExecuteNonQuery();
            connection.Close();
        }

        public List<Produto> listaProduto()
        {

            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("select id, nome, descricao, quantidade, id_fornecedor, id_departamento from produto order by nome");
                query.Connection = connection.Open();
                using (NpgsqlDataReader rs = query.ExecuteReader())
                    if (rs.HasRows)
                    {
                        List<Produto> produtos = new List<Produto>();
                        while (rs.Read())
                        {
                            Produto p = new Produto();
                            p.id = rs.GetInt32(0);
                            p.nome = rs.GetString(1);
                            p.descricao = rs.GetString(2);
                            p.quantidade = rs.GetInt32(3);
                            p.id_fornecedor = rs.GetInt32(4);
                            p.id_departamento = rs.GetInt32(5);
                            produtos.Add(p);
                        }
                        connection.Close();
                        return produtos;
                    }
                connection.Close();
                return null;

            } catch (Exception ex)
            {
                Console.WriteLine("Erro de conexão" + ex.Message);
                return null;
            }
            
        }

        public List<Produto> listaProduto(string nome)
        {
            Conexao connection = new Conexao();
            NpgsqlCommand query = new NpgsqlCommand("select id, nome, descricao, quantidade, id_fornecedor, id_departamento from produto where nome LIKE @nome order by nome");
            query.Connection = connection.Open();
            query.Parameters.Add("nome", NpgsqlTypes.NpgsqlDbType.Varchar).Value = "%" + nome + "%";
            using (NpgsqlDataReader rs = query.ExecuteReader())
                if (rs.HasRows)
                {
                    List<Produto> produtos = new List<Produto>();
                    while (rs.Read())
                    {
                        Produto p = new Produto();
                        p.id = rs.GetInt32(0);
                        p.nome = rs.GetString(1);
                        p.descricao = rs.GetString(2);
                        p.quantidade = rs.GetInt32(3);
                        p.id_fornecedor = rs.GetInt32(4);
                        p.id_departamento = rs.GetInt32(5);
                        produtos.Add(p);
                    }
                    connection.Close();
                    return produtos;
                }
            connection.Close();
            return null;
        }

        public void adicionar(Produto produto, int qtde)
        {
            Conexao connection = new Conexao();
            NpgsqlCommand query = new NpgsqlCommand("UPDATE produto SET quantidade = quantidade+ @qtde WHERE id = @id");
            query.Connection = connection.Open();
            query.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = produto.id;
            query.Parameters.Add("qtde", NpgsqlTypes.NpgsqlDbType.Integer).Value = qtde;
            query.ExecuteNonQuery();
            connection.Close();
        }

        public void retirar(Produto produto, int qtde) {
            Conexao connection = new Conexao();
            NpgsqlCommand query = new NpgsqlCommand("UPDATE produto SET quantidade = quantidade - @qtde WHERE id = @id");
            query.Connection = connection.Open();
            query.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = produto.id;
            query.Parameters.Add("qtde", NpgsqlTypes.NpgsqlDbType.Integer).Value = qtde;
            query.ExecuteNonQuery();
            connection.Close();
        }
      

    }
}

