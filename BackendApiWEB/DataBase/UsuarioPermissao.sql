USE [SistemaPDV];
GO

CREATE TABLE [dbo].[UsuarioPermissao](
    [Id] INT IDENTITY(1,1) NOT NULL,
    [UsuarioId] UNIQUEIDENTIFIER NOT NULL,
    [PermissaoId] INT NOT NULL,
    CONSTRAINT [PK_UsuarioPermissao] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_Usuario_Permissao] UNIQUE NONCLUSTERED (
        [UsuarioId] ASC,
        [PermissaoId] ASC
    )
);
GO

ALTER TABLE [dbo].[UsuarioPermissao]  WITH CHECK 
ADD CONSTRAINT [FK_UsuarioPermissao_Permissao] 
FOREIGN KEY([PermissaoId])
REFERENCES [dbo].[Permissoes] ([Id])
ON DELETE CASCADE;
GO

ALTER TABLE [dbo].[UsuarioPermissao] CHECK CONSTRAINT [FK_UsuarioPermissao_Permissao];
GO

ALTER TABLE [dbo].[UsuarioPermissao]  WITH CHECK 
ADD CONSTRAINT [FK_UsuarioPermissao_Usuario] 
FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
ON DELETE CASCADE;
GO

ALTER TABLE [dbo].[UsuarioPermissao] CHECK CONSTRAINT [FK_UsuarioPermissao_Usuario];
GO
