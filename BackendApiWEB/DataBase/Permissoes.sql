-- =====================================================
-- TABELA: Permissoes
-- Descrição: Armazena os tipos de permissões do sistema
-- Autor: Seu Nome
-- Data de Criação: 14/12/2025
-- Última Modificação: 14/12/2025
-- =====================================================

USE [SistemaPDV]
GO

-- Verificar se a tabela já existe e remover (cuidado em produção!)
IF OBJECT_ID('[dbo].[Permissoes]', 'U') IS NOT NULL
BEGIN
    PRINT 'Removendo tabela existente: Permissoes'
    DROP TABLE [dbo].[Permissoes]
END
GO

-- Criar a tabela
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Permissoes](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Nome] [varchar](100) NOT NULL,
    [Descricao] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (
    PAD_INDEX = OFF, 
    STATISTICS_NORECOMPUTE = OFF, 
    IGNORE_DUP_KEY = OFF, 
    ALLOW_ROW_LOCKS = ON, 
    ALLOW_PAGE_LOCKS = ON, 
    OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
    [Nome] ASC
)WITH (
    PAD_INDEX = OFF, 
    STATISTICS_NORECOMPUTE = OFF, 
    IGNORE_DUP_KEY = OFF, 
    ALLOW_ROW_LOCKS = ON, 
    ALLOW_PAGE_LOCKS = ON, 
    OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Inserir dados padrão
INSERT INTO [dbo].[Permissoes] ([Nome], [Descricao])
VALUES 
    ('ADMIN', 'Administrador com acesso total ao sistema'),
    ('USUARIO', 'Usuário comum com acesso limitado')
GO

PRINT 'Tabela Permissoes criada com sucesso!'
GO