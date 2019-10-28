insert into entrada (quantidade, id_produto,data,horario) values(3, 1, '25/10/2019', '17:32:00')

select produto.nome, entrada.id, entrada.quantidade, entrada.data, entrada.horario
from entrada join produto on produto.id = entrada.id_produto
order by produto.nome;
