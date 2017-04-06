
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/06/2017 15:00:53
-- Generated from EDMX file: E:\Projects\GitHub\WebSiteMusicBand_CMS\WebSiteMusicBand\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MusicBandSiteDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Comments_Posts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_Comments_Posts];
GO
IF OBJECT_ID(N'[dbo].[FK_Comments_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_Comments_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Posts_users1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_Posts_users1];
GO
IF OBJECT_ID(N'[dbo].[FK_Entity2Entity1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts_Entity1] DROP CONSTRAINT [FK_Entity2Entity1];
GO
IF OBJECT_ID(N'[dbo].[FK_Entity1_inherits_Posts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts_Entity1] DROP CONSTRAINT [FK_Entity1_inherits_Posts];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Entity2Set]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entity2Set];
GO
IF OBJECT_ID(N'[dbo].[Posts_Entity1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts_Entity1];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [CommentID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [PostID] int  NOT NULL,
    [Text] nchar(1000)  NOT NULL,
    [CreateTime] datetime  NOT NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [PostID] int IDENTITY(1,1) NOT NULL,
    [Title] nchar(100)  NOT NULL,
    [Text] nchar(1000)  NULL,
    [Discriminator] int  NULL,
    [UserID] int  NOT NULL,
    [CreateTime] datetime  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [Login] nchar(50)  NOT NULL,
    [Password] nchar(50)  NOT NULL,
    [Avatar] varbinary(max)  NULL,
    [Birthday] datetime  NOT NULL,
    [Email] nchar(50)  NOT NULL
);
GO

-- Creating table 'Entity2Set'
CREATE TABLE [dbo].[Entity2Set] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [filepath] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Posts_Entity1'
CREATE TABLE [dbo].[Posts_Entity1] (
    [EntID] nvarchar(max)  NOT NULL,
    [PostID] int  NOT NULL,
    [Entity2_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CommentID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([CommentID] ASC);
GO

-- Creating primary key on [PostID] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([PostID] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [Id] in table 'Entity2Set'
ALTER TABLE [dbo].[Entity2Set]
ADD CONSTRAINT [PK_Entity2Set]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [PostID] in table 'Posts_Entity1'
ALTER TABLE [dbo].[Posts_Entity1]
ADD CONSTRAINT [PK_Posts_Entity1]
    PRIMARY KEY CLUSTERED ([PostID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PostID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_Comments_Posts]
    FOREIGN KEY ([PostID])
    REFERENCES [dbo].[Posts]
        ([PostID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Comments_Posts'
CREATE INDEX [IX_FK_Comments_Posts]
ON [dbo].[Comments]
    ([PostID]);
GO

-- Creating foreign key on [UserID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_Comments_Users]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Comments_Users'
CREATE INDEX [IX_FK_Comments_Users]
ON [dbo].[Comments]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_Posts_users1]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Posts_users1'
CREATE INDEX [IX_FK_Posts_users1]
ON [dbo].[Posts]
    ([UserID]);
GO

-- Creating foreign key on [Entity2_Id] in table 'Posts_Entity1'
ALTER TABLE [dbo].[Posts_Entity1]
ADD CONSTRAINT [FK_Entity2Entity1]
    FOREIGN KEY ([Entity2_Id])
    REFERENCES [dbo].[Entity2Set]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Entity2Entity1'
CREATE INDEX [IX_FK_Entity2Entity1]
ON [dbo].[Posts_Entity1]
    ([Entity2_Id]);
GO

-- Creating foreign key on [PostID] in table 'Posts_Entity1'
ALTER TABLE [dbo].[Posts_Entity1]
ADD CONSTRAINT [FK_Entity1_inherits_Posts]
    FOREIGN KEY ([PostID])
    REFERENCES [dbo].[Posts]
        ([PostID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------