CREATE TABLE [dbo].[Nomer] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (MAX) NOT NULL,
    [plata_za_den] FLOAT (53)     NOT NULL,
    CONSTRAINT [PK_Nomer] PRIMARY KEY CLUSTERED ([Id] ASC)
);

