USE [BookKeeperDB_Test]
GO

/****** Object:  Table [dbo].[book_author]    Script Date: 8/27/2024 3:37:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[book_author](
	[BOOK_ID] [int] NOT NULL,
	[AUTHOR_ID] [int] NOT NULL,
 CONSTRAINT [PK_book_author] PRIMARY KEY CLUSTERED 
(
	[BOOK_ID] ASC,
	[AUTHOR_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[book_author]  WITH CHECK ADD  CONSTRAINT [FK_book_author_author] FOREIGN KEY([AUTHOR_ID])
REFERENCES [dbo].[author] ([AUTHOR_ID])
GO

ALTER TABLE [dbo].[book_author] CHECK CONSTRAINT [FK_book_author_author]
GO

ALTER TABLE [dbo].[book_author]  WITH CHECK ADD  CONSTRAINT [FK_book_author_book] FOREIGN KEY([BOOK_ID])
REFERENCES [dbo].[book] ([BOOK_ID])
GO

ALTER TABLE [dbo].[book_author] CHECK CONSTRAINT [FK_book_author_book]
GO

