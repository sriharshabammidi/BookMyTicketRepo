CREATE TABLE [BookMyTicket].[Reservations] (
    [ID]       BIGINT IDENTITY (1, 1) NOT NULL,
    [TicketID] BIGINT NULL,
    [SeatID]   BIGINT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ReservationSeat] FOREIGN KEY ([SeatID]) REFERENCES [BookMyTicket].[CinemaSeating] ([ID]),
    CONSTRAINT [FK_ReservationTicket] FOREIGN KEY ([TicketID]) REFERENCES [BookMyTicket].[Tickets] ([ID])
);

