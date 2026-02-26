CREATE TABLE dbo.Produtos (
  Id uniqueidentifier NOT NULL PRIMARY KEY,
  Nome nvarchar(200) NOT NULL,
  Descricao nvarchar(max) NULL,
  Preco decimal(18,2) NOT NULL,
  Estoque int NOT NULL,
  Ativo bit NOT NULL,
  CriadoEm datetime2 NOT NULL,
  AtualizadoEm datetime2 NULL
);

/*
CREATE TABLE dbo.Produtos (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Nome          NVARCHAR(200) NOT NULL,
    Descricao     NVARCHAR(1000) NULL,
    Preco         DECIMAL(18,2) NOT NULL DEFAULT(0),
    Estoque       INT NOT NULL DEFAULT(0),
    Ativo         BIT NOT NULL DEFAULT(1),
    CriadoEm      DATETIME2 NOT NULL DEFAULT(SYSUTCDATETIME()),
    AtualizadoEm  DATETIME2 NULL
);


**/