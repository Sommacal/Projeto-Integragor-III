using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace Zeit
{
   public class UsuarioDAO
    {
        public bool ValidarLogin(string cpf, string senha)
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
        public string GetByID(string Cpf)
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("select nome FROM usuario WHERE cpf=@cpf");
                query.Connection = connection.Open();
                query.Parameters.Add("cpf", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Cpf;
                using (NpgsqlDataReader rs = query.ExecuteReader())
                    if (rs.HasRows)
                    {
                        string nome = string.Empty;
                        while (rs.Read())
                        {
                            nome = rs.GetString(0);
                        }
                        connection.Close();
                        return nome;
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
