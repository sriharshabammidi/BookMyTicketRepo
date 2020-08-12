CREATE TABLE [BookMyTicket].[Tickets] (
    [ID]     BIGINT       IDENTITY (1, 1) NOT NULL,
    [ShowID] BIGINT       NULL,
    [UserID] BIGINT       NULL,
    [Price]  DECIMAL (18) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_TicketShow] FOREIGN KEY ([ShowID]) REFERENCES [BookMyTicket].[Shows] ([ID]),
    CONSTRAINT [FK_TicketUser] FOREIGN KEY ([UserID]) REFERENCES [BookMyTicket].[Users] ([Id])
);

