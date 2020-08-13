CREATE TABLE [BookMyTicket].[Users] (
    [Id]        BIGINT       IDENTITY (1, 1) NOT NULL,
    [FirstName] VARCHAR (50) NULL,
    [LastName]  VARCHAR (50) NULL,
    [Email]     VARCHAR (50) NOT NULL,
    [Password]  VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [constraint_email] UNIQUE NONCLUSTERED ([Email] ASC)
);



