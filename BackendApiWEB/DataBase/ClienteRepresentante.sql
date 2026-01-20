CREATE TABLE dbo.ClienteFornecedor 
(
    ClienteId       INT NOT NULL,
    FornecedorId INT NOT NULL,
    CONSTRAINT PK_ClienteFornecedor PRIMARY KEY (ClienteId, FornecedorId),
    CONSTRAINT FK_CR_Cliente FOREIGN KEY (ClienteId) REFERENCES dbo.Pessoas(Id),
    CONSTRAINT FK_CR_Fornecedor FOREIGN KEY (FornecedorId) REFERENCES dbo.Pessoas(Id)
);

GO
