CREATE TABLE [BookMyTicket].[Cinemas] (
    [ID]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]     VARCHAR (255) NULL,
    [LayoutID] BIGINT        NOT NULL,
    [CityID]   BIGINT        NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CinemaCity] FOREIGN KEY ([CityID]) REFERENCES [BookMyTicket].[Cities] ([ID])
);

