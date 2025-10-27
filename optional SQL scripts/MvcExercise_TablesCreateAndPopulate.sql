USE [MvcExercise]

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

-- Create StatusValues table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StatusValues]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StatusValues](
	[ID] [uniqueidentifier] NOT NULL,
	[StatusValue] [nvarchar](4) NOT NULL,
 CONSTRAINT [PK_StatusValues] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_StatusValues_ID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[StatusValues] ADD  CONSTRAINT [DF_StatusValues_ID]  DEFAULT (newid()) FOR [ID]
END
GO

-- Create SimpleDataSet table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SimpleDataSet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SimpleDataSet](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Rank] [nvarchar](100) NULL,
	[Position] [nvarchar](100) NULL,
	[Posting] [nvarchar](100) NULL,
	[UpdateTimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SimpleDataSet] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_SimpleDataSet_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_SimpleDataSet_UpdateTimeStamp]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SimpleDataSet] ADD  CONSTRAINT [DF_SimpleDataSet_UpdateTimeStamp]  DEFAULT (getdate()) FOR [UpdateTimeStamp]
END
GO


-- Populate StatusValues table
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StatusValues]') AND type in (N'U'))
BEGIN
	DECLARE @num INT, @i INT = 0
	WHILE @i < 37
	BEGIN
		SET @i = @i + 1
		SET @num = CEILING(RAND()*3)
    
		INSERT INTO [dbo].[StatusValues]
			SELECT NEWID(), CHOOSE(@num, 'Fail', 'Warn', 'Pass')
	END	
END
GO


-- Populate SimpleDataSet table
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SimpleDataSet]') AND type in (N'U'))
BEGIN
	INSERT INTO [dbo].[SimpleDataSet]
			   ([Name]
			   ,[UpdateTimeStamp]
			   ,[Rank]
			   ,[Posting]
			   ,[Position])
	VALUES
		(N'Hikaru Sulu', GETDATE(), N'Lieutenant', N'USS Enterprise', N'Helmsman')
		,(N'James Tiberius Kirk', GETDATE(), N'Captain', N'USS Enterprise', N'Commanding Officer')
		,(N'Leonard "Bones" McCoy', GETDATE(), N'Lieutenant Commander', N'USS Enterprise', N'Chief Medical Officer')
		,(N'Montgomery "Scotty" Scott', GETDATE(), N'Lieutenant Commander', N'USS Enterprise', N'Chief Engineer')
		,(N'Nyota Uhura', GETDATE(), N'Lieutenant', N'USS Enterprise', N'Communications Officer')
		,(N'Pavel Chekov', GETDATE(), N'Ensign', N'USS Enterprise', N'Navigator')
		,(N'Spock', GETDATE(), N'Commander', N'USS Enterprise', N'First Officer')
END
GO
