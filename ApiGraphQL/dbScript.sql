CREATE DATABASE graphqlmicroservice;

\c graphqlmicroservice;

CREATE TABLE Genero (
    id SERIAL NOT NULL PRIMARY KEY,
    nome varchar(30) NOT NULL
);

alter table genero
add constraint unique_nome_genero UNIQUE(nome);

CREATE TABLE Estudio (
    id SERIAL NOT NULL PRIMARY KEY,
    nome varchar(100) NOT NULL
);

CREATE TABLE Jogo (
    id SERIAL NOT NULL PRIMARY KEY,
    nome varchar(50) NOT NULL,
    descricao varchar(1000) NULL,
    classificacaoESBR int NOT NULL,
    idEstudio integer,
    FOREIGN KEY(idEstudio) references Estudio(id) on delete set null
);

CREATE TABLE public.JogoGenero (
    idJogo integer NOT NULL,
    idGenero integer NOT NULL,
    PRIMARY KEY(idJogo, idGenero),
    FOREIGN KEY(idGenero) references Genero(id) on delete cascade,
    FOREIGN KEY(idJogo) references Jogo(id) on delete cascade
);
