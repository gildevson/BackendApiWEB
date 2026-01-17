CREATE TABLE dbo.Pessoas (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    TipoPessoa   VARCHAR(30) NOT NULL,      -- CLIENTE / REPRESENTANTE

    CpfCnpj      VARCHAR(20) NOT NULL,
    RazaoSocial  VARCHAR(200) NOT NULL,
    Fantasia     VARCHAR(200) NULL,

    Cep          VARCHAR(10)  NULL,
    Logradouro   VARCHAR(200) NULL,
    Numero       VARCHAR(20)  NULL,
    Complemento  VARCHAR(200) NULL,
    Bairro       VARCHAR(100) NULL,
    Cidade       VARCHAR(100) NULL,
    Estado       VARCHAR(2)   NULL,

    NomeContato  VARCHAR(120) NULL,
    Email        VARCHAR(150) NULL,
    Telefone1    VARCHAR(30)  NULL,

    CONSTRAINT UQ_Pessoas_Tipo_CpfCnpj UNIQUE (TipoPessoa, CpfCnpj)
);
GO
