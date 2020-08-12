CREATE TABLE [BookMyTicket].[Movies] (
    [ID]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (255) NULL,
    [Duration]    INT           NULL,
    [Description] VARCHAR (255) NULL,
    [Poster]      VARCHAR (255) NULL,
    [ReleaseDate] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

