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
            try
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
            }catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao inserir: " + ex.Message);
            }            
        }

        public List<Fornecedor> listafornecedores()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Erro de banco de dados: " + ex.Message);
            }            
        }
        public Fornecedor GetByID(int Id)
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("Select id, nome, cnpj, email, telefone, endereco from fornecedor WHERE id = @id");
                query.Connection = connection.Open();
                query.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Id;
                using (NpgsqlDataReader rs = query.ExecuteReader())
                    if (rs.HasRows)
                    {
                        Fornecedor f = new Fornecedor();
                        while (rs.Read())
                        {
                            f.id = rs.GetInt32(0);
                            f.nome = rs.GetString(1);
                            f.cnpj = rs.GetString(2);
                            f.email = rs.GetString(3);
                            f.telefone = rs.GetString(4);
                            f.endereco = rs.GetString(5);
                        }
                        connection.Close();
                        return f;
                    }
                connection.Close();
                return null;
            }catch (Exception ex)
            {
               throw new Exception("Erro de banco de dados: " + ex.Message);
            } 
        }
        public void Update(Fornecedor fornecedor)
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("update fornecedor set nome=@nome," +
                " cnpj=@cnpj, email=@email, telefone=@telefone, endereco=@endereco" +
                " where id=@id");
                query.Connection = connection.Open();
                query.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = fornecedor.id;
                query.Parameters.Add("nome", NpgsqlTypes.NpgsqlDbType.Varchar).Value = fornecedor.nome;
                query.Parameters.Add("cnpj", NpgsqlTypes.NpgsqlDbType.Varchar).Value = fornecedor.cnpj;
                query.Parameters.Add("email", NpgsqlTypes.NpgsqlDbType.Varchar).Value = fornecedor.email;
                query.Parameters.Add("telefone", NpgsqlTypes.NpgsqlDbType.Varchar).Value = fornecedor.telefone;
                query.Parameters.Add("endereco", NpgsqlTypes.NpgsqlDbType.Varchar).Value = fornecedor.endereco;
                query.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de banco de dados: " + ex.Message);
            }
        }
        public void Delete(int id)
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("DELETE from fornecedor where id=@id");
                query.Connection = connection.Open();
                query.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
                query.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de banco de dados: " + ex.Message);
            }
        }
    }
}
