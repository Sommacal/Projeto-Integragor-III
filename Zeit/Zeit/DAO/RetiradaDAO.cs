using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace Zeit
{
    public class RetiradaDAO
    {
        public void retirada (Retirada retirada)
        {
            Conexao connection = new Conexao();
            NpgsqlCommand query = new NpgsqlCommand("INSERT INTO retirada (quantidade, id_produto, data, horario) values (@quantidade, @id_produto, @data, @horario)");
            query.Connection = connection.Open();
            query.Parameters.Add("quantidade", NpgsqlTypes.NpgsqlDbType.Integer).Value = retirada.quantidade;
            query.Parameters.Add("id_produto", NpgsqlTypes.NpgsqlDbType.Integer).Value = retirada.id_produto;
            query.Parameters.Add("data", NpgsqlTypes.NpgsqlDbType.Date).Value = retirada.data;
            query.Parameters.Add("horario", NpgsqlTypes.NpgsqlDbType.Time).Value = retirada.horario;
            query.ExecuteNonQuery();
            connection.Close();
        }
        
    }
}
