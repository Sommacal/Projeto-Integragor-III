using System;
using System.Collections.Generic;
using Npgsql;
using System.Data;

namespace Zeit
{
    public class DepartamentoDAO
    {
        public void inserir(Departamento departamento)
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("INSERT INTO departamento (nome, descricao) values (@nome, @descricao)");
                query.CommandType = CommandType.Text;
                query.Connection = connection.Open();
                query.Parameters.Add("nome", NpgsqlTypes.NpgsqlDbType.Varchar).Value = departamento.nome;
                query.Parameters.Add("descricao", NpgsqlTypes.NpgsqlDbType.Varchar).Value = departamento.descricao;
                query.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao inserir: " + ex.Message);
            }
        }
        public List<Departamento> listadepartamento()
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("select id, nome, descricao from departamento");
                query.Connection = connection.Open();
                using (NpgsqlDataReader rs = query.ExecuteReader())
                    if (rs.HasRows)
                    {
                        List<Departamento> departamentos = new List<Departamento>();
                        while (rs.Read())
                        {
                            Departamento d = new Departamento();
                            d.id = rs.GetInt32(0);
                            d.nome = rs.GetString(1);
                            d.descricao = rs.GetString(2);

                            departamentos.Add(d);
                        }
                        connection.Close();
                        return departamentos;
                    }
                connection.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de banco de dados: " + ex.Message);
            }
        }
        public Departamento GetByID(int Id)
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("Select id, nome, descricao from departamento WHERE id = @id");
                query.Connection = connection.Open();
                query.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Id;
                using (NpgsqlDataReader rs = query.ExecuteReader())
                    if (rs.HasRows)
                    {
                        Departamento d = new Departamento();
                        while (rs.Read())
                        {
                            d.id = rs.GetInt32(0);
                            d.nome = rs.GetString(1);
                            d.descricao = rs.GetString(2);

                        }
                        connection.Close();
                        return d;
                    }
                connection.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de banco de dados: " + ex.Message);
            }

        }

        public void Update(Departamento departamento)
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("update departamento set nome=@nome, descricao=@descricao where id=@id");
                query.Connection = connection.Open();
                query.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = departamento.id;
                query.Parameters.Add("nome", NpgsqlTypes.NpgsqlDbType.Varchar).Value = departamento.nome;
                query.Parameters.Add("descricao", NpgsqlTypes.NpgsqlDbType.Varchar).Value = departamento.descricao;
                query.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de banco de dados" + ex.Message);
            }
        }
        public void Delete(int id)
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("DELETE from departamento where id=@id");
                query.Connection = connection.Open();
                query.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
                query.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de banco de dados" + ex.Message);
            }
        }
    }
}
