CREATE TABLE [BookMyTicket].[CinemaSeating] (
    [ID]           BIGINT       IDENTITY (1, 1) NOT NULL,
    [SeatNumber]   VARCHAR (10) NULL,
    [RowNumber]    INT          NULL,
    [ColumnNumber] INT          NULL,
    [LayoutID]     BIGINT       NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

