USE [BookKeeperDB_Test]
GO

/****** Object:  Table [dbo].[author]    Script Date: 8/27/2024 3:37:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[author](
	[AUTHOR_ID] [int] IDENTITY(1,1) NOT NULL,
	[FIRST_NAME] [nvarchar](max) NOT NULL,
	[MIDDLE_NAME] [nvarchar](max) NULL,
	[LAST_NAME] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_author] PRIMARY KEY CLUSTERED 
(
	[AUTHOR_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

