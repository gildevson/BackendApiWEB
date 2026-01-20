CREATE TABLE dbo.ClienteRepresentante (
    ClienteId       INT NOT NULL,
    RepresentanteId INT NOT NULL,
    CONSTRAINT PK_ClienteRepresentante PRIMARY KEY (ClienteId, RepresentanteId),
    CONSTRAINT FK_CR_Cliente FOREIGN KEY (ClienteId) REFERENCES dbo.Pessoas(Id),
    CONSTRAINT FK_CR_Representante FOREIGN KEY (RepresentanteId) REFERENCES dbo.Pessoas(Id)
);
GO
