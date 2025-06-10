-- REMOVE E CRIA O BANCO DE DADOS
DROP DATABASE IF EXISTS Biblioteca;
CREATE DATABASE Biblioteca;
USE Biblioteca;

-- TABELA 1: Autor
CREATE TABLE autor (
    cod_aut INT NOT NULL PRIMARY KEY,
    nome_aut VARCHAR(100) NOT NULL UNIQUE
);

-- TABELA 2: Editora
CREATE TABLE editora (
    cod_edit INT NOT NULL PRIMARY KEY,
    nome_edit VARCHAR(50) NOT NULL UNIQUE
);

-- TABELA 3: Categoria
CREATE TABLE categoria (
    cod_cat INT NOT NULL PRIMARY KEY,
    nome_cat VARCHAR(100) NOT NULL UNIQUE
);

-- TABELA 4: Estado
CREATE TABLE estado (
    sigla_est CHAR(2) NOT NULL PRIMARY KEY,
    nome_est VARCHAR(50) NOT NULL UNIQUE
);

-- TABELA 5: Cidade
CREATE TABLE cidade (
    cod_cidade INT NOT NULL PRIMARY KEY,
    sigla_est CHAR(2) NOT NULL,
    nome_cid VARCHAR(100) NOT NULL,
    CONSTRAINT fk_estado FOREIGN KEY (sigla_est) REFERENCES estado(sigla_est)
);

-- TABELA 6: Cliente
CREATE TABLE cliente (
    cod_cli INT NOT NULL PRIMARY KEY,
    cod_cid INT NOT NULL,
    nome_cli VARCHAR(200) NOT NULL,
    endereco_cli VARCHAR(200) NOT NULL,
    sexo_cli CHAR(1) NOT NULL CHECK (sexo_cli IN ('F', 'M')),
    FOREIGN KEY (cod_cid) REFERENCES cidade(cod_cidade)
);

-- TABELA 7: Título
CREATE TABLE titulo (
    cod_tit INT NOT NULL PRIMARY KEY,
    cod_cat INT NOT NULL,
    cod_edit INT NOT NULL,
    nome_livro VARCHAR(100) NOT NULL UNIQUE,
    val_livro DECIMAL(10,2) NOT NULL CHECK (val_livro > 0),
    qtd_estq INT NOT NULL CHECK (qtd_estq >= 0),
    FOREIGN KEY (cod_cat) REFERENCES categoria(cod_cat),
    FOREIGN KEY (cod_edit) REFERENCES editora(cod_edit)
);

-- TABELA 8: Pedido
CREATE TABLE pedido (
    cod_ped INT NOT NULL PRIMARY KEY,
    cod_cli INT NOT NULL,
    data_ped DATETIME NOT NULL,
    val_ped DECIMAL(10,2) NOT NULL CHECK (val_ped > 0),
    FOREIGN KEY (cod_cli) REFERENCES cliente(cod_cli)
);

-- TABELA 9: Título_Pedido
-- CREATE TABLE titulo_pedido (
--     cod_ped INT NOT NULL,
--     cod_tit INT NOT NULL,
--     qtd_livro INT NOT NULL CHECK (qtd_livro >= 1),
--     val_livro DECIMAL(10,2) NOT NULL CHECK (val_livro > 0),
--     PRIMARY KEY (cod_ped, cod_tit),
--     FOREIGN KEY (cod_ped) REFERENCES pedido(cod_ped),
--     FOREIGN KEY (cod_tit) REFERENCES titulo(cod_tit)
-- );

-- -- TABELA 10: Título_Autor
-- CREATE TABLE titulo_autor (
--     cod_tit INT NOT NULL,
--     cod_aut INT NOT NULL,
--     PRIMARY KEY (cod_tit, cod_aut),
--     FOREIGN KEY (cod_tit) REFERENCES titulo(cod_tit),
--     FOREIGN KEY (cod_aut) REFERENCES autor(cod_aut)
-- );


CREATE TABLE livro (
    cod_livro INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    titulo VARCHAR(100) NOT null,
    categoria VARCHAR(45) NOT NULL,
    autor VARCHAR(100) NOT NULL,
    editora VARCHAR(100) NOT NULL,
    quantidade INT NOT NULL,
    preco DECIMAL(10,2) NOT NULL CHECK (preco > 0)
);


