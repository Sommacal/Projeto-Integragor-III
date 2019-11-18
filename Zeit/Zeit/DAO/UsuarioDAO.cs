using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace Zeit
{
   public class UsuarioDAO
    {
        public bool ValidarLogin(String cpf, String senha)
        {
            Conexao conn = new Conexao();
            NpgsqlCommand query = new NpgsqlCommand("select cpf, senha from usuario where cpf = @cpf and senha = @senha");
            query.Parameters.AddWithValue("@cpf", cpf);
            query.Parameters.AddWithValue("@senha", senha);
            try
            {
                query.Connection = conn.Open();
                using (NpgsqlDataReader rs = query.ExecuteReader())
                    if (rs.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
