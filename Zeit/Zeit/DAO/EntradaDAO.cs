using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace Zeit
{
    public class EntradaDAO
    {

        public void entrada (Entrada entrada)
        {
            Conexao connection = new Conexao();
            NpgsqlCommand query = new NpgsqlCommand("INSERT INTO entrada (quantidade, id_produto, data, horario) values (@quantidade, @id_produto, @data, @horario)");
            query.Connection = connection.Open();
            query.Parameters.Add("quantidade", NpgsqlTypes.NpgsqlDbType.Integer).Value = entrada.quantidade;
            query.Parameters.Add("id_produto", NpgsqlTypes.NpgsqlDbType.Integer).Value = entrada.id_produto;
            query.Parameters.Add("data", NpgsqlTypes.NpgsqlDbType.Date).Value = entrada.data;
            query.Parameters.Add("horario", NpgsqlTypes.NpgsqlDbType.Time).Value = entrada.horario;
            query.ExecuteNonQuery();
            connection.Close();
        }

    }
}
