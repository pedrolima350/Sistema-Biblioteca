DROP DATABASE IF EXISTS Biblioteca;
CREATE DATABASE Biblioteca;
USE Biblioteca;

-- TABELA 1: Autor
CREATE TABLE autor (
    cod_aut INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome_aut VARCHAR(100) NOT NULL UNIQUE
);

-- TABELA 2: Editora
CREATE TABLE editora (
    cod_edit INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome_edit VARCHAR(50) NOT NULL UNIQUE
);

-- TABELA 3: Categoria
CREATE TABLE categoria (
    cod_cat INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome_cat VARCHAR(100) NOT NULL UNIQUE
);

-- TABELA 6: Cliente
CREATE TABLE cliente (
    cod_cli INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome_cli VARCHAR(200) NOT NULL,
    sexo_cli CHAR(1) NOT NULL CHECK (sexo_cli IN ('F', 'M'))
);

-- TABELA 8: Pedido
CREATE TABLE pedido (
    cod_ped INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    cod_cli INT NOT NULL,
    data_ped DATETIME NOT NULL,
    val_ped DECIMAL(10,2) NOT NULL CHECK (val_ped > 0),
    FOREIGN KEY (cod_cli) REFERENCES cliente(cod_cli)
);

-- TABELA DE LIVROS MODIFICADA PARA UTILIZAR CHAVES ESTRANGEIRAS
CREATE TABLE livro (

    cod_livro INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    titulo VARCHAR(100) NOT null,
    categoria VARCHAR(45) NOT NULL,
    autor VARCHAR(100) NOT NULL,
    editora VARCHAR(100) NOT NULL,
    quantidade INT NOT NULL,
    preco DECIMAL(10,2) NOT NULL CHECK (preco > 0)

);