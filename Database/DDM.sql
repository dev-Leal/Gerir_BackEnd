-- Inserir novos dados a tabela com (insert into, values (NEWID))
-- NEWID()- Gera um uniqueindentifier para o id 
insert into Usuarios (id,nome,email,senha,tipo)
values (NEWID(), 'Beatriz Leal', 'beatriz.leal@gmail.com', '123456', 'Comum')

insert into Usuarios
values (NEWID(), 'Pricilla Medeiros', 'primedeiros@gmail.com', '123456', 'Comum')

-- aletra todas as linhas / usar o WHERE ID para alterar uma unica linha
update Usuarios set tipo = 'Comum'

select * from Usuarios -- * retorna todos os campos
select nome, email from Usuarios


insert into Tarefas (id,titulo,descricao,categoria,DataEntrega,status,usuario_id)
values (NEWID(), 'Tarefa 1', 'Descrição Tarefa 1', 'Pessoal', '04-01-2020 18:00:00', 0 , 'FCC79931-FD98-435E-9E7F-773DEDE5316F')

insert into Tarefas (id,titulo,descricao,categoria,DataEntrega,status,usuario_id)
values (NEWID(), 'Tarefa 1', 'Descrição Tarefa 1', 'Pessoal', '04-01-2020 18:00:00', 0 , 'FCC79931-FD98-435E-9E7F-773DEDE5316F')

select * from Tarefas 
select * from Usuarios

--inner join 

select u.id as id_usuario,
	u.nome,
	u.email,
	t.id as id_tarefa,
	t.titulo,
	t.descricao,
	t.categoria,
	t.status,
	t.dataentrega
from Usuarios u 
inner join tarefas t on t.usuario_id = u.id
