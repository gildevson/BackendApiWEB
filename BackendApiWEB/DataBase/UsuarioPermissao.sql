USE [SistemaPDV]
GO

/****** Object:  Table [dbo].[UsuarioPermissao]    Script Date: 14/12/2025 17:54:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UsuarioPermissao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [uniqueidentifier] NOT NULL,
	[PermissaoId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_Usuario_Permissao] UNIQUE NONCLUSTERED 
(
	[UsuarioId] ASC,
	[PermissaoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UsuarioPermissao]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioPermissao_Permissao] FOREIGN KEY([PermissaoId])
REFERENCES [dbo].[Permissoes] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UsuarioPermissao] CHECK CONSTRAINT [FK_UsuarioPermissao_Permissao]
GO

ALTER TABLE [dbo].[UsuarioPermissao]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioPermissao_Usuario] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UsuarioPermissao] CHECK CONSTRAINT [FK_UsuarioPermissao_Usuario]
GO


