CREATE TABLE dbo.ProdutoPrecoHistorico (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProdutoId UNIQUEIDENTIFIER NOT NULL,
    Preco DECIMAL(18,2) NOT NULL,
    VigenciaInicio DATETIME2 NOT NULL,
    VigenciaFim DATETIME2 NULL,
    CONSTRAINT FK_PPH_Produto FOREIGN KEY (ProdutoId) REFERENCES dbo.Produtos(Id)
);
GO