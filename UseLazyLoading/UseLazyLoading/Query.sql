IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Client] (
    [ClientId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [Email] nvarchar(25) NOT NULL,
    CONSTRAINT [PK_Client] PRIMARY KEY ([ClientId])
);
GO

CREATE TABLE [Office] (
    [OfficeId] int NOT NULL IDENTITY,
    [Title] nvarchar(100) NOT NULL,
    [Location] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Office] PRIMARY KEY ([OfficeId])
);
GO

CREATE TABLE [Title] (
    [TitleId] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Title] PRIMARY KEY ([TitleId])
);
GO

CREATE TABLE [Project] (
    [ProjectId] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [Budget] money NOT NULL,
    [StartedDate] datetime2(7) NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY ([ProjectId]),
    CONSTRAINT [FK_Project_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Client] ([ClientId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Employees] (
    [EmployeeId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [HiredDate] datetime2 NOT NULL,
    [DateOfBirth] date NOT NULL,
    [OfficeId] int NOT NULL,
    [TitleId] int NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([EmployeeId]),
    CONSTRAINT [FK_Employees_Office_OfficeId] FOREIGN KEY ([OfficeId]) REFERENCES [Office] ([OfficeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Employees_Title_TitleId] FOREIGN KEY ([TitleId]) REFERENCES [Title] ([TitleId]) ON DELETE CASCADE
);
GO

CREATE TABLE [EmployeeProject] (
    [EmployeeProjectId] int NOT NULL IDENTITY,
    [Rate] money NOT NULL,
    [StartedDate] datetime2(7) NOT NULL,
    [EmployeeId] int NOT NULL,
    [ProjectId] int NOT NULL,
    CONSTRAINT [PK_EmployeeProject] PRIMARY KEY ([EmployeeProjectId]),
    CONSTRAINT [FK_EmployeeProject_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([EmployeeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_EmployeeProject_Project_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Project] ([ProjectId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_EmployeeProject_EmployeeId] ON [EmployeeProject] ([EmployeeId]);
GO

CREATE INDEX [IX_EmployeeProject_ProjectId] ON [EmployeeProject] ([ProjectId]);
GO

CREATE INDEX [IX_Employees_OfficeId] ON [Employees] ([OfficeId]);
GO

CREATE INDEX [IX_Employees_TitleId] ON [Employees] ([TitleId]);
GO

CREATE UNIQUE INDEX [IX_Project_ClientId] ON [Project] ([ClientId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230808181852_InitialCreate', N'7.0.10');
GO

COMMIT;
GO

