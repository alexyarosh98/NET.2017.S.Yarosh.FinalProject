
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/28/2017 19:10:20
-- Generated from EDMX file: D:\TaskJobSystem\EF_ORM\TJSystemContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TJS_DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Category_1toM_Task]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Category_1toM_Task];
GO
IF OBJECT_ID(N'[dbo].[FK_Task_1to1_TaskInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Task_1to1_TaskInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTaskInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TaskInfoes] DROP CONSTRAINT [FK_UserTaskInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoes] DROP CONSTRAINT [FK_UserUserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_TaskInfoUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TaskInfoes] DROP CONSTRAINT [FK_TaskInfoUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[UserInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoes];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Tasks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tasks];
GO
IF OBJECT_ID(N'[dbo].[TaskInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TaskInfoes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [Nickname] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Role] tinyint  NOT NULL,
    [Rating] float  NOT NULL
);
GO

-- Creating table 'UserInfoes'
CREATE TABLE [dbo].[UserInfoes] (
    [UserInfoId] int IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(max)  NULL,
    [Lastname] nvarchar(max)  NULL,
    [Age] smallint  NULL,
    [Gender] bit  NULL,
    [User_UserId] int  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [CategoryId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Tasks'
CREATE TABLE [dbo].[Tasks] (
    [TaskId] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [Status] tinyint  NOT NULL,
    [CategoryId] int  NOT NULL,
    [TaskInfo_TaskInfoId] int  NOT NULL
);
GO

-- Creating table 'TaskInfoes'
CREATE TABLE [dbo].[TaskInfoes] (
    [TaskInfoId] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Deadline] datetime  NOT NULL,
    [Implementation] smallint  NOT NULL,
    [CreatorUserId] int  NOT NULL,
    [Developer_UserId] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [UserInfoId] in table 'UserInfoes'
ALTER TABLE [dbo].[UserInfoes]
ADD CONSTRAINT [PK_UserInfoes]
    PRIMARY KEY CLUSTERED ([UserInfoId] ASC);
GO

-- Creating primary key on [CategoryId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([CategoryId] ASC);
GO

-- Creating primary key on [TaskId] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [PK_Tasks]
    PRIMARY KEY CLUSTERED ([TaskId] ASC);
GO

-- Creating primary key on [TaskInfoId] in table 'TaskInfoes'
ALTER TABLE [dbo].[TaskInfoes]
ADD CONSTRAINT [PK_TaskInfoes]
    PRIMARY KEY CLUSTERED ([TaskInfoId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryId] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [FK_Category_1toM_Task]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Category_1toM_Task'
CREATE INDEX [IX_FK_Category_1toM_Task]
ON [dbo].[Tasks]
    ([CategoryId]);
GO

-- Creating foreign key on [TaskInfo_TaskInfoId] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [FK_Task_1to1_TaskInfo]
    FOREIGN KEY ([TaskInfo_TaskInfoId])
    REFERENCES [dbo].[TaskInfoes]
        ([TaskInfoId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Task_1to1_TaskInfo'
CREATE INDEX [IX_FK_Task_1to1_TaskInfo]
ON [dbo].[Tasks]
    ([TaskInfo_TaskInfoId]);
GO

-- Creating foreign key on [CreatorUserId] in table 'TaskInfoes'
ALTER TABLE [dbo].[TaskInfoes]
ADD CONSTRAINT [FK_UserTaskInfo]
    FOREIGN KEY ([CreatorUserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTaskInfo'
CREATE INDEX [IX_FK_UserTaskInfo]
ON [dbo].[TaskInfoes]
    ([CreatorUserId]);
GO

-- Creating foreign key on [User_UserId] in table 'UserInfoes'
ALTER TABLE [dbo].[UserInfoes]
ADD CONSTRAINT [FK_UserUserInfo]
    FOREIGN KEY ([User_UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserInfo'
CREATE INDEX [IX_FK_UserUserInfo]
ON [dbo].[UserInfoes]
    ([User_UserId]);
GO

-- Creating foreign key on [Developer_UserId] in table 'TaskInfoes'
ALTER TABLE [dbo].[TaskInfoes]
ADD CONSTRAINT [FK_TaskInfoUser]
    FOREIGN KEY ([Developer_UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TaskInfoUser'
CREATE INDEX [IX_FK_TaskInfoUser]
ON [dbo].[TaskInfoes]
    ([Developer_UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------