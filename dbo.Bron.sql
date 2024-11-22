CREATE TABLE [dbo].[Bron] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [data_zaezd] DATETIME2 (7)  NOT NULL,
    [data_viezd] DATETIME2 (7)  NOT NULL,
    [UserId]     INT            NOT NULL,
    [NomerId]    INT            NOT NULL,
    CONSTRAINT [PK_Bron] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Bron_Nomer_NomerId] FOREIGN KEY ([NomerId]) REFERENCES [dbo].[Nomer] ([Id]),
    CONSTRAINT [FK_Bron_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Bron_NomerId]
    ON [dbo].[Bron]([NomerId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Bron_UserId]
    ON [dbo].[Bron]([UserId] ASC);

