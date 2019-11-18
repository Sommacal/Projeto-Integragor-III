using System;
using System.Collections.Generic;
using Npgsql;

namespace Zeit
{
    public class RetiradaDAO
    {
        public void retirada (Retirada retirada)
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("INSERT INTO retirada (quantidade, id_produto, data, horario, cpf_usuario) values (@quantidade, @id_produto, @data, @horario, @cpf_usuario)");
                query.Connection = connection.Open();
                query.Parameters.Add("quantidade", NpgsqlTypes.NpgsqlDbType.Integer).Value = retirada.quantidade;
                query.Parameters.Add("id_produto", NpgsqlTypes.NpgsqlDbType.Integer).Value = retirada.id_produto;
                query.Parameters.Add("data", NpgsqlTypes.NpgsqlDbType.Date).Value = retirada.data;
                query.Parameters.Add("horario", NpgsqlTypes.NpgsqlDbType.Time).Value = retirada.horario;
                query.Parameters.Add("cpf_usuario", NpgsqlTypes.NpgsqlDbType.Varchar).Value = retirada.cpf_usuario;
                query.ExecuteNonQuery();
                connection.Close();
            }catch (Exception ex)
            {
                throw new Exception("Erro ao inserir" + ex.Message);
            }            
        }
        public List<Retirada> GetAll()
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("select id, quantidade, id_produto, data, horario from retirada");
                query.Connection = connection.Open();
                using (NpgsqlDataReader rs = query.ExecuteReader())
                    if (rs.HasRows)
                    {
                        List<Retirada> retiradas = new List<Retirada>();
                        while (rs.Read())
                        {
                            Retirada r = new Retirada();
                            r.id = rs.GetInt32(0);
                            r.quantidade = rs.GetInt32(1);
                            r.id_produto = rs.GetInt32(2);
                            r.data = rs.GetDateTime(3);
                            r.horario = rs.GetTimeSpan(4);
                            retiradas.Add(r);
                        }
                        connection.Close();
                        return retiradas;
                    }
                connection.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de banco de dados" + ex.Message);
            }
        }
        public List<Retirada> GetLastFive()
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("select retirada.quantidade, retirada.data, retirada.horario, produto.nome, usuario.nome from retirada join produto on retirada.id_produto = produto.id join usuario on retirada.cpf_usuario = usuario.cpf order by retirada.id desc limit 5");
                query.Connection = connection.Open();
                using (NpgsqlDataReader rs = query.ExecuteReader())
                    if (rs.HasRows)
                    {
                        List<Retirada> lastfive = new List<Retirada>();
                        Random rnd = new Random();
                        while (rs.Read())
                        {
                            Retirada r = new Retirada();
                            r.quantidade = rs.GetInt32(0);
                            r.data = rs.GetDateTime(1);
                            r.horario = rs.GetTimeSpan(2);
                            r.nome_produto = rs.GetString(3);
                            r.nome_usuario = rs.GetString(4);
                            lastfive.Add(r);
                        }
                        connection.Close();
                        return lastfive;
                    }
                connection.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de banco de dados" + ex.Message);
            }
        }
    }
 }
