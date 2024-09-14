-- Criação do banco de dados
CREATE DATABASE DBNoticias;
GO

-- Utilizando o banco de dados recém-criado
USE DBNoticias;
GO

-- Criação da tabela de Autores
CREATE TABLE [TbAutor](
    [AutorId] INT IDENTITY(1,1) NOT NULL,
    [Nome] VARCHAR(150) NOT NULL,
    [Email] VARCHAR(150) NOT NULL,
 CONSTRAINT [PK_TbAutor] PRIMARY KEY CLUSTERED 
(
    [AutorId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
    ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
) ON [PRIMARY];
GO

-- Criação da tabela de Notícias
CREATE TABLE [TbNoticia](
    [NotId] INT IDENTITY(1,1) NOT NULL,
    [Titulo] VARCHAR(200) NOT NULL,
    [Texto] TEXT NOT NULL,
    [Data] DATETIME NOT NULL,
    [AutorId] INT NOT NULL,
 CONSTRAINT [PK_TbNoticia] PRIMARY KEY CLUSTERED 
(
    [NotId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
    ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
) ON [PRIMARY];
GO

-- Criação da chave estrangeira para associar a tabela de Notícias com a tabela de Autores
ALTER TABLE [TbNoticia]  WITH CHECK ADD  CONSTRAINT [FK_TbNoticia_TbAutor] FOREIGN KEY([AutorId])
REFERENCES [TbAutor] ([AutorId]);
GO

-- Verificação da restrição de chave estrangeira
ALTER TABLE [TbNoticia] CHECK CONSTRAINT [FK_TbNoticia_TbAutor];
GO
