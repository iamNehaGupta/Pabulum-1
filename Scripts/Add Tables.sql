-- Login Table
CREATE TABLE [dbo].[Login] (
	[Id] BIGINT IDENTITY (1, 1) NOT NULL,
	
	-- [PersonId] BIGINT NULL,

	[UserName] NVARCHAR(250) NOT NULL,
	[Password] NVARCHAR(250) NOT NULL,

	-- MAINTAINENCE COLUMNS
	[CreatedBy] BIGINT NOT NULL,
	[CreatedOnDate] DATETIME NOT NULL,
	[LastModifiedBy] BIGINT NOT NULL,
	[LastModifiedOnDate] DATETIME NOT NULL,

    [RowVer] ROWVERSION NOT NULL,
	
    CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Login_Login_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Login] ([Id]),
	CONSTRAINT [FK_Login_Login_LastModifiedBy] FOREIGN KEY ([LastModifiedBy]) REFERENCES [dbo].[Login] ([Id]),
	-- CONSTRAINT [FK_Login_Person_PersonID] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person]([Id]),
);

-- company table
CREATE TABLE [dbo].[Company](
	[Id] BIGINT IDENTITY(1, 1) NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[Description] NVARCHAR(500) NULL,

	[IsDeleted] BIT NOT NULL CONSTRAINT [DF_Company_IsDeleted] DEFAULT (0),

	-- MAINTAINENCE COLUMNS
	[CreatedBy] BIGINT NOT NULL,
	[CreatedOnDate] DATETIME NOT NULL,
	[LastModifiedBy] BIGINT NOT NULL,
	[LastModifiedOnDate] DATETIME NOT NULL,

    [RowVer] ROWVERSION NOT NULL,

	CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Company_Login_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Login] ([Id]),
	CONSTRAINT [FK_Company_Login_LastModifiedBy] FOREIGN KEY ([LastModifiedBy]) REFERENCES [dbo].[Login] ([Id])
);

-- person table
-- holds all the details requred for a person
CREATE TABLE [dbo].[Person] (
	[Id] BIGINT IDENTITY (1, 1) NOT NULL,
    [CompanyId] BIGINT NOT NULL,
	[FirstName] NVARCHAR(200) NOT NULL,
	[LastName] NVARCHAR(200) NULL,
	[MiddleName] NVARCHAR(200) NULL,
	[DOB] DATETIME NOT NULL,
	[GENDER] NVARCHAR(1) NOT NULL, -- M (male), F (female), U (Not specifed)
	[Type] NVARCHAR(1) NOT NULL,
	[IsDeleted] BIT NOT NULL CONSTRAINT [DF_Person_IsDeleted] DEFAULT (0),

	-- MAINTAINENCE COLUMNS
	[CreatedBy] BIGINT NOT NULL,
	[CreatedOnDate] DATETIME NOT NULL,
	[LastModifiedBy] BIGINT NOT NULL,
	[LastModifiedOnDate] DATETIME NOT NULL,

    [RowVer] ROWVERSION NOT NULL,

	CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Person_Login_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Login] ([Id]),
	CONSTRAINT [FK_Person_Login_LastModifiedBy] FOREIGN KEY ([LastModifiedBy]) REFERENCES [dbo].[Login] ([Id]),
    CONSTRAINT [FK_Person_Company_CreatedBy] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id])
);


-- write a script to add personid in login table

