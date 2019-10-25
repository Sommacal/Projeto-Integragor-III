using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace Zeit
{
    public class DepartamentoDAO
    {

        public void inserir (Departamento departamento)
        {
            Conexao connection = new Conexao();
            NpgsqlCommand query = new NpgsqlCommand("INSERT INTO departamento (nome, descricao) values (@nome, @descricao)");
            query.Connection = connection.Open();
            query.Parameters.Add("nome", NpgsqlTypes.NpgsqlDbType.Varchar).Value = departamento.nome;
            query.Parameters.Add("descricao", NpgsqlTypes.NpgsqlDbType.Varchar).Value = departamento.descricao;            
            query.ExecuteNonQuery();
            connection.Close();
        }

        public List<Departamento> listadepartamento()
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
    }
}
