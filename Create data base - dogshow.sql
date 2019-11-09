create table categoria (
   id serial primary key not null,
   nome varchar (30) not null,
   status boolean not null
); 

create table adicional(
   id serial primary key not null,
   nome varchar(30) not null, 
   valor float not null,
   status boolean not null
);
create table produto (
	id  serial primary key not null,
	nome varchar (50) not null,
	valor float not null,
	status boolean not null,
	id_categoria integer,
	foreign key (id_categoria) references categoria(id)
);

create table pedido (
	id serial primary key not null,
	data date not null,  
	valor float not null,
	observacao varchar (200)
	nome varchar (50) not null, 	
	status boolean not null,
);

create table produtopedido (
	id_pedido integer,
	id_produto integer,
	quantidade integer,
	foreign key (id_pedido) references pedido(id),
	foreign key (id_produto) references produto(id),
	primary key (id_pedido, id_produto)
);

create table adicionalproduto(
	id_pedido integer, 
	id_produto integer,
	id_adicional integer,
	quantidade integer,
	foreign key (id_pedido) references pedido(id),
	foreign key (id_produto) references produto(id),
	foreign key (id_adicional) references adicional (id),
	primary key (id_pedido, id_produto, id_adicional)
);