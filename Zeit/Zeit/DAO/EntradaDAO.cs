using System;
using System.Collections.Generic;
using Npgsql;

namespace Zeit
{
    public class EntradaDAO
    {

        public void entrada (Entrada entrada)
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("INSERT INTO entrada (quantidade, id_produto, data, horario, cpf_usuario) values (@quantidade, @id_produto, @data, @horario, @cpf_usuario)");
                query.Connection = connection.Open();
                query.Parameters.Add("quantidade", NpgsqlTypes.NpgsqlDbType.Integer).Value = entrada.quantidade;
                query.Parameters.Add("id_produto", NpgsqlTypes.NpgsqlDbType.Integer).Value = entrada.id_produto;
                query.Parameters.Add("data", NpgsqlTypes.NpgsqlDbType.Date).Value = entrada.data;
                query.Parameters.Add("horario", NpgsqlTypes.NpgsqlDbType.Time).Value = entrada.horario;
                query.Parameters.Add("cpf_usuario", NpgsqlTypes.NpgsqlDbType.Varchar).Value = entrada.cpf_usuario;
                query.ExecuteNonQuery();
                connection.Close();
            }catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao inserir: " + ex.Message);
            }            
        }            
        public List<Entrada> GetAll()
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("select id, quantidade, id_produto, data, horario from entrada");
                query.Connection = connection.Open();
                using (NpgsqlDataReader rs = query.ExecuteReader())
                    if (rs.HasRows)
                    {
                        List<Entrada> entradas = new List<Entrada>();
                        while (rs.Read())
                        {
                            Entrada e = new Entrada();
                            e.id = rs.GetInt32(0);
                            e.quantidade = rs.GetInt32(1);
                            e.id_produto = rs.GetInt32(2);
                            e.data = rs.GetDateTime(3);
                            e.horario = rs.GetTimeSpan(4);
                            entradas.Add(e);
                        }
                        connection.Close();
                        return entradas;
                    }
                connection.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de banco de dados: "+ex.Message);
            }
        }

        public List<Entrada> GetLastFive()
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("select entrada.quantidade, entrada.data, entrada.horario, produto.nome, usuario.nome from entrada join produto on entrada.id_produto = produto.id join usuario on entrada.cpf_usuario = usuario.cpf order by entrada.id desc limit 5");
                query.Connection = connection.Open();
                using (NpgsqlDataReader rs = query.ExecuteReader())
                    if (rs.HasRows)
                    {
                        List<Entrada> lastfive = new List<Entrada>();
                        Random rnd = new Random();
                        while (rs.Read())
                        {
                            Entrada e = new Entrada();
                            e.quantidade = rs.GetInt32(0);
                            e.data = rs.GetDateTime(1);
                            e.horario = rs.GetTimeSpan(2);
                            e.nome_produto = rs.GetString(3);
                            e.nome_usuario = rs.GetString(4);
                            lastfive.Add(e);
                        }
                        connection.Close();
                        return lastfive;
                    }
                connection.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de banco de dados: " + ex.Message);
            }
        }
    }
}
