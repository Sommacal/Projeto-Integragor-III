using System;
using System.Collections.Generic;
using System.Text;
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
               connection.ConnectionString = (ConnectionString);           
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());                
            }
        }    
        public NpgsqlConnection Open (){
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return connection;
        }

        public void Close(){
            connection.Close();
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
	qtde integer,
    id_produto integer,
	data date,
    horario time,
	foreign key(id_produto) references produto(id)
    );*/

