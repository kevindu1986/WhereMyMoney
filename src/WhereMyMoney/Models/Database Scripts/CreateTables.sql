USE [WhereMyMoney]
GO

ALTER TABLE [dbo].[Tbl_Trace] DROP CONSTRAINT [FK_Tbl_Trace_Tbl_User]
GO

ALTER TABLE [dbo].[Tbl_Trace] DROP CONSTRAINT [FK_Tbl_Trace_Tbl_Currency]
GO

DROP TABLE [dbo].[Tbl_Trace]
GO

DROP TABLE [dbo].[Tbl_Currency]
GO

DROP TABLE [dbo].[Tbl_User]
GO

CREATE TABLE [dbo].[Tbl_Currency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CurrencyName] [varchar](50) NOT NULL,
	[CurrencyShortName] [varchar](4) NOT NULL,
	[Description] [ntext] NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Tbl_User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](20) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Tbl_Trace](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TraceDate] [date] NOT NULL,
	[UserId] [int] NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[Description] [ntext] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Tbl_Trace] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tbl_Trace]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Trace_Tbl_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Tbl_Currency] ([Id])
GO

ALTER TABLE [dbo].[Tbl_Trace] CHECK CONSTRAINT [FK_Tbl_Trace_Tbl_Currency]
GO

ALTER TABLE [dbo].[Tbl_Trace]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Trace_Tbl_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Tbl_User] ([Id])
GO

ALTER TABLE [dbo].[Tbl_Trace] CHECK CONSTRAINT [FK_Tbl_Trace_Tbl_User]
GO
