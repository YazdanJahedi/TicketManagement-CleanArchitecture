

CREATE TABLE [dbo].[FAQCategories] (
    [Id]           BIGINT        NOT NULL,
    [CategoryName]        VARCHAR (MAX) NOT NULL,
    [CreationDate] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[FAQItems] (
    [Id]           BIGINT        NOT NULL,
    [CategoryId]   BIGINT        NOT NULL, -- FK
    [Title]        VARCHAR (MAX) NULL, 
    [Description]  VARCHAR (MAX) NULL,
    [CreationDate] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY (CategoryId) REFERENCES FAQCategories(Id)

);


CREATE TABLE [dbo].[Messages] (
    [Id]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [TicketId]     BIGINT        NOT NULL, -- FK
    [CreatorEmail]    VARCHAR(MAX)        NOT NULL,
    [Text]         VARCHAR (MAX) NULL,
    [CreationDate] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY (TicketId) REFERENCES Tickets(Id),
    
);


CREATE TABLE [dbo].[Tickets] (
    [Id]                BIGINT        IDENTITY (1, 1) NOT NULL,
    [CreatorId]         BIGINT        NOT NULL, -- FK
    [Title]             VARCHAR (MAX) NOT NULL,
    [Description]       VARCHAR (MAX) NOT NULL,
    [FirstResponseDate] DATETIME      NULL,
    [CloseDate]         DATETIME      NULL,
    [CreationDate]      DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY (CreatorId) REFERENCES Users(Id)

);


CREATE TABLE [dbo].[Users] (
    [Id]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (MAX) NULL,
    [Email]        VARCHAR (MAX) NOT NULL,
    [Role]         VARCHAR (MAX) NOT NULL,
    [PasswordHash] VARCHAR (MAX) NOT NULL,
    [PhoneNumber]  VARCHAR (MAX) NULL,
    [CreationDate] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

