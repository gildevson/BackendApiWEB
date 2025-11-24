CREATE TABLE Permissoes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(200)
);

INSERT INTO Permissoes (Nome, Descricao)
VALUES ('ADMIN', 'Administrador do sistema');

INSERT INTO Permissoes (Nome, Descricao)
VALUES ('USUARIO', 'Acesso básico ao sistema');