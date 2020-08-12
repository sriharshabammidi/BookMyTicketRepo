CREATE TABLE [BookMyTicket].[Shows] (
    [ID]           BIGINT       IDENTITY (1, 1) NOT NULL,
    [ShowTime]     DATETIME     NULL,
    [MovieID]      BIGINT       NULL,
    [CinemaID]     BIGINT       NULL,
    [PricePerUnit] DECIMAL (18) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ShowCinema] FOREIGN KEY ([MovieID]) REFERENCES [BookMyTicket].[Cinemas] ([ID]),
    CONSTRAINT [FK_ShowMovie] FOREIGN KEY ([MovieID]) REFERENCES [BookMyTicket].[Movies] ([ID])
);

