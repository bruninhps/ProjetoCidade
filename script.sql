CREATE DATABASE EcommerceNovo;
USE EcommerceNovo;

CREATE TABLE Usuario(
	Id int primary key auto_increment, 
    Nome varchar(50) not null,
    Email varchar(50) not null,
    Senha varchar (20) not null
);

CREATE TABLE Produto(
	Id int primary key auto_increment,
    Nome varchar(50) not null,
    Descricao varchar(200) not null,
    Quantidade int not null,
    Preco decimal (12,2)
);

insert into Usuario (Nome, Email, Senha) values ('Bruno', 'bruno@gmail.com', '2393');