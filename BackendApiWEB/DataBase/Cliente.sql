USE [SistemaPDV]
GO

/****** Object:  Table [dbo].[Clientes]    Script Date: 31/03/2026 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Clientes](
	[Id]             [uniqueidentifier] NOT NULL,
	[Nome]           [nvarchar](150)    NOT NULL,
	[CnpjCpf]        [varchar](18)      NULL,
	[Email]          [varchar](200)     NULL,
	[Telefone]       [varchar](20)      NULL,
	[Endereco]       [nvarchar](300)    NULL,
	[Cidade]         [nvarchar](100)    NULL,
	[Estado]         [varchar](2)       NULL,
	[CEP]            [varchar](9)       NULL,
	[Ativo]          [bit]              NOT NULL,
	[CriadoEm]       [datetime2]        NOT NULL,
	[AtualizadoEm]   [datetime2]        NULL,
PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Clientes] ADD DEFAULT (newid())    FOR [Id]
GO
ALTER TABLE [dbo].[Clientes] ADD DEFAULT (1)          FOR [Ativo]
GO
ALTER TABLE [dbo].[Clientes] ADD DEFAULT (getdate())  FOR [CriadoEm]
GO



ALTER TABLE dbo.clientes ADD Bairro nvarchar(100) null;
AlTER TABLE dbo.clientes ADD Numero varchar(20) null;
ALTER TABLE dbo.clientes ADD Complemento nvarchar(100)null;