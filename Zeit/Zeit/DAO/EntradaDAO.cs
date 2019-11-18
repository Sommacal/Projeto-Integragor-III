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
        public List<Microcharts.Entry> Relatorio1()
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("select entrada.quantidade, produto.nome from entrada join produto on entrada.id_produto = produto.id order by entrada.quantidade desc limit 5");
                query.Connection = connection.Open();
                using (NpgsqlDataReader rs = query.ExecuteReader())
                    if (rs.HasRows)
                    {
                        List<Microcharts.Entry> relatorio = new List<Microcharts.Entry>();
                        Random rnd = new Random();
                        while (rs.Read())
                        {
                            Microcharts.Entry m = new Microcharts.Entry(rs.GetInt32(0));
                            m.ValueLabel = Convert.ToString(rs.GetInt32(0));
                            m.Label = rs.GetString(1);
                            m.Color = SkiaSharp.SKColor.Parse(String.Format("#{0:X6}", rnd.Next(0x1000000)));
                            relatorio.Add(m);
                        }
                        connection.Close();
                        return relatorio;
                    }
                connection.Close();
                return null;
            }
             catch (Exception ex)
            {
                throw new Exception("Erro ao carregar relatório: " + ex.Message);
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
    }
}
