using System;
using Npgsql;

namespace Zeit
{
    class Conexao
    {
        

        string ConnectionString = "Server=10.0.2.2;Port=5432;Database=estoque;User Id=postgres;Password=1234;";

        NpgsqlConnection connection = new NpgsqlConnection();
        public Conexao()
        {
            try
            {
                connection.ConnectionString = ConnectionString;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de conexão: " + ex.Message);
            }
        }
        public NpgsqlConnection Open()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao abrir conexão: " + ex.Message);
            }
            return connection;
        }
        public void Close()
        {
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao fechar conexão: " + ex.Message);
            }
        }
    }
}

/*create table departamento(
	id serial primary key not null,
	nome varchar(30),
	descricao varchar (50)
	);*/

/*create table fornecedor(
    id serial primary key not null,
    nome varchar(40),
	cnpj varchar(50),
	email varchar(70),
	telefone varchar(20),
	endereco varchar(150)
	);*/

/*create table produto(
	id serial primary key not null,
	nome varchar(30) not null,
	descricao varchar (50),
	quantidade integer not null,
	validade date,
	id_fornecedor integer,
	id_departamento integer,
	FOREIGN KEY (id_fornecedor) REFERENCES fornecedor(id),
	foreign key (id_departamento) references departamento(id)	
	);*/

// UPDATE produto SET quantidade = quantidade+ @qtde WHERE id = @id

/*create table entrada(
    id serial primary key not null,
	quantidade integer,
    id_produto integer,
	data date,
    horario time,
	foreign key(id_produto) references produto(id)
    );*/

/*create table retirada(
    id serial primary key not null,
    quantidade integer,
    id_produto integer,
    data date,
    horario time,
    foreign key(id_produto) references produto(id)
    );*/


/*SELECT ULTIMAS ENTRADAS
 * select produto.nome, entrada.quantidade 
from entrada join produto on id_produto = produto.id
order by data desc
limit 3*/

/*
 * select entrada.id, entrada.quantidade, entrada.data, entrada.horario, produto.nome
from entrada join produto on entrada.id_produto = produto.id
order by produto.nome desc limit 5;
 */
