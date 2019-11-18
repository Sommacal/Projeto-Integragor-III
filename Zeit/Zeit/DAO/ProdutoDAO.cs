using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace Zeit
{
    public class ProdutoDAO
    {
        public void inserir(Produto produto)
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("INSERT INTO produto (nome, descricao, quantidade, validade, id_fornecedor, id_departamento) values (@nome, @descricao, @quantidade, @validade, @id_fornecedor, @id_departamento)");
                query.Connection = connection.Open();
                query.Parameters.Add("nome", NpgsqlTypes.NpgsqlDbType.Varchar).Value = produto.nome;
                query.Parameters.Add("descricao", NpgsqlTypes.NpgsqlDbType.Varchar).Value = produto.descricao;
                query.Parameters.Add("quantidade", NpgsqlTypes.NpgsqlDbType.Integer).Value = produto.quantidade;
                query.Parameters.Add("id_fornecedor", NpgsqlTypes.NpgsqlDbType.Integer).Value = produto.id_fornecedor;
                query.Parameters.Add("id_departamento", NpgsqlTypes.NpgsqlDbType.Integer).Value = produto.id_departamento;
                if (produto.validade is null)
                {
                    query.Parameters.Add("validade", NpgsqlTypes.NpgsqlDbType.Date).Value = DBNull.Value;
                }
                else
                {
                    query.Parameters.Add("validade", NpgsqlTypes.NpgsqlDbType.Date).Value = produto.validade;
                }
                query.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao inserir: " + ex.Message);
            }
        }

        public List<Produto> listaProduto()
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("select id, nome, descricao, quantidade, id_fornecedor, id_departamento from produto order by nome");
                query.Connection = connection.Open();
                using (NpgsqlDataReader rs = query.ExecuteReader())
                    if (rs.HasRows)
                    {
                        List<Produto> produtos = new List<Produto>();
                        while (rs.Read())
                        {
                            Produto p = new Produto();
                            p.id = rs.GetInt32(0);
                            p.nome = rs.GetString(1);
                            p.descricao = rs.GetString(2);
                            p.quantidade = rs.GetInt32(3);
                            p.id_fornecedor = rs.GetInt32(4);
                            p.id_departamento = rs.GetInt32(5);
                            produtos.Add(p);
                        }
                        connection.Close();
                        return produtos;
                    }
                connection.Close();
                return null;

            } catch (Exception ex)
            {
                throw new Exception("Erro de banco de dados" + ex.Message);
            }            
        }
        public List<Produto> listaProduto(string nome)
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("select id, nome, descricao, quantidade, id_fornecedor, id_departamento from produto where nome LIKE @nome order by nome");
                query.Connection = connection.Open();
                query.Parameters.Add("nome", NpgsqlTypes.NpgsqlDbType.Varchar).Value = "%" + nome + "%";
                using (NpgsqlDataReader rs = query.ExecuteReader())
                    if (rs.HasRows)
                    {
                        List<Produto> produtos = new List<Produto>();
                        while (rs.Read())
                        {
                            Produto p = new Produto();
                            p.id = rs.GetInt32(0);
                            p.nome = rs.GetString(1);
                            p.descricao = rs.GetString(2);
                            p.quantidade = rs.GetInt32(3);
                            p.id_fornecedor = rs.GetInt32(4);
                            p.id_departamento = rs.GetInt32(5);
                            produtos.Add(p);
                        }
                        connection.Close();
                        return produtos;
                    }
                        connection.Close();
                        return null;
            }catch(Exception ex)
            {
                throw new Exception("Erro de banco de dados" + ex.Message);
            }
        }
        public void adicionar(Produto produto, int qtde)
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("UPDATE produto SET quantidade = quantidade+ @qtde WHERE id = @id");
                query.Connection = connection.Open();
                query.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = produto.id;
                query.Parameters.Add("qtde", NpgsqlTypes.NpgsqlDbType.Integer).Value = qtde;
                query.ExecuteNonQuery();
                connection.Close();
            }catch(Exception ex)
            {
                throw new Exception("Erro de banco de dados" + ex.Message);
            }
        }
        public void retirar(Produto produto, int qtde) {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("UPDATE produto SET quantidade = quantidade - @qtde WHERE id = @id");
                query.Connection = connection.Open();
                query.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = produto.id;
                query.Parameters.Add("qtde", NpgsqlTypes.NpgsqlDbType.Integer).Value = qtde;
                query.ExecuteNonQuery();
                connection.Close();
            }catch(Exception ex)
            {
                throw new Exception("Erro de banco de dados" + ex.Message);
            }
        }
        public Produto GetByID(int Id)
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("Select id, nome, descricao, quantidade, validade, id_fornecedor, id_departamento from produto WHERE id = @id");
                query.Connection = connection.Open();
                query.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Id;
                using (NpgsqlDataReader rs = query.ExecuteReader())
                    if (rs.HasRows)
                    {
                        Produto p = new Produto();
                        while (rs.Read())
                        {
                            p.id = rs.GetInt32(0);
                            p.nome = rs.GetString(1);
                            p.descricao = rs.GetString(2);
                            p.quantidade = rs.GetInt32(3);
                            if (rs.IsDBNull(4))
                            {
                                p.validade = null;
                            }
                            else
                            {
                                p.validade = rs.GetDateTime(4);
                            }
                            p.id_fornecedor = rs.GetInt32(5);
                            p.id_departamento = rs.GetInt32(6);
                        }
                        connection.Close();
                        return p;
                    }
                connection.Close();
                return null;
            }catch (Exception ex)
            {
                throw new Exception("Erro de banco de dados" + ex.Message);
            }            
        }
        public void Update(Produto produto)
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("update produto set nome=@nome," +
                " descricao=@descricao, id_fornecedor=@id_fornecedor, id_departamento=@id_departamento, validade=@validade" +
                " where id=@id");
                query.Connection = connection.Open();
                query.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = produto.id;
                query.Parameters.Add("nome", NpgsqlTypes.NpgsqlDbType.Varchar).Value = produto.nome;
                query.Parameters.Add("descricao", NpgsqlTypes.NpgsqlDbType.Varchar).Value = produto.descricao;
                query.Parameters.Add("id_fornecedor", NpgsqlTypes.NpgsqlDbType.Integer).Value = produto.id_fornecedor;
                query.Parameters.Add("id_departamento", NpgsqlTypes.NpgsqlDbType.Integer).Value = produto.id_departamento;
                if (produto.validade is null)
                {
                    query.Parameters.Add("validade", NpgsqlTypes.NpgsqlDbType.Date).Value = DBNull.Value;
                }
                else
                {
                    query.Parameters.Add("validade", NpgsqlTypes.NpgsqlDbType.Date).Value = produto.validade;
                }
                query.ExecuteNonQuery();
                connection.Close();
            }catch (Exception ex)
            {
                throw new Exception("Erro de banco de dados" + ex.Message);
            }                                
        }

        public void Delete(int id)
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("DELETE from produto where id=@id");
                query.Connection = connection.Open();
                query.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
                query.ExecuteNonQuery();
                connection.Close();
            }catch(Exception ex)
            {
                throw new Exception("Erro de banco de dados" + ex.Message);
            }
        }

        public List<Microcharts.Entry> Relatorio1()
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("select quantidade, nome from produto order by quantidade desc limit 4");
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

        public List<Microcharts.Entry> Relatorio2()
        {
            try
            {
                Conexao connection = new Conexao();
                NpgsqlCommand query = new NpgsqlCommand("select quantidade, nome from produto order by quantidade asc limit 4");
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


    }
}


