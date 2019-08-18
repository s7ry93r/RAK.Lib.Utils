USE [C:\DEV\VS\MVCA\APP_DATA\MVCA.MDF]
GO

/****** Object: Table [dbo].[People] Script Date: 8/13/2019 9:07:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[People] (
    [Id]          INT          NOT NULL,
    [name]        VARCHAR (20) NULL,
    [create_date] DATETIME     NULL,
    [comment]     VARCHAR (50) NULL
);


