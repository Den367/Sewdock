/* Before executing this script:
    - Replace {DBNAME} with the name of your database (e.g. Mayando).
    - Replace {TABLEPREFIX} with the optional (unique) prefix for all database
      tables; use when multiple application instances are installed in the same
      database. Remove the {TABLEPREFIX} text everywhere if you don't need a table
      prefix.
*/

USE {DBNAME}

/****** Object:  Table [dbo].[{TABLEPREFIX}Log]    Script Date: 08/21/2009 11:28:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{TABLEPREFIX}Log]') AND type in (N'U'))
BEGIN
CREATE TABLE [{TABLEPREFIX}Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Time] [datetime] NOT NULL,
	[Level] [int] NOT NULL,
	[Message] [nvarchar](max) COLLATE Latin1_General_CI_AS NOT NULL,
	[Detail] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_{TABLEPREFIX}Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[{TABLEPREFIX}Menu]    Script Date: 07/08/2009 16:11:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{TABLEPREFIX}Menu]') AND type in (N'U'))
BEGIN
CREATE TABLE [{TABLEPREFIX}Menu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Sequence] [int] NOT NULL,
	[OpenInNewWindow] [bit] NOT NULL,
	[Title] [nvarchar](max) COLLATE Latin1_General_CI_AS NOT NULL,
	[Url] [nvarchar](max) COLLATE Latin1_General_CI_AS NOT NULL,
	[ToolTip] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_{TABLEPREFIX}Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[{TABLEPREFIX}Tag]    Script Date: 07/08/2009 16:11:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{TABLEPREFIX}Tag]') AND type in (N'U'))
BEGIN
CREATE TABLE [{TABLEPREFIX}Tag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_{TABLEPREFIX}Tag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[{TABLEPREFIX}Setting]    Script Date: 07/08/2009 16:11:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{TABLEPREFIX}Setting]') AND type in (N'U'))
BEGIN
CREATE TABLE [{TABLEPREFIX}Setting](
	[Scope] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[Name] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[Type] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[UserVisible] [bit] NOT NULL,
	[Sequence] [int] NULL,
	[Title] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[Value] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_{TABLEPREFIX}Setting] PRIMARY KEY CLUSTERED 
(
	[Scope] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[{TABLEPREFIX}Photo]    Script Date: 07/08/2009 16:11:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{TABLEPREFIX}Photo]') AND type in (N'U'))
BEGIN
CREATE TABLE [{TABLEPREFIX}Photo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExternalId] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[ExternalUrl] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[Title] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[Text] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[UrlLarge] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[UrlNormal] [nvarchar](max) COLLATE Latin1_General_CI_AS NOT NULL,
	[UrlSmall] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[UrlThumbnail] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[UrlThumbnailSquare] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[DateTaken] [datetime] NULL,
	[DatePublished] [datetime] NOT NULL,
	[Hidden] [bit] NOT NULL,
 CONSTRAINT [PK_{TABLEPREFIX}Photo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[{TABLEPREFIX}Photo]') AND name = N'IX_{TABLEPREFIX}Photo_DatePublished')
CREATE NONCLUSTERED INDEX [IX_{TABLEPREFIX}Photo_DatePublished] ON [{TABLEPREFIX}Photo] 
(
	[DatePublished] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[{TABLEPREFIX}Gallery]    Script Date: 07/08/2009 16:11:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{TABLEPREFIX}Gallery]') AND type in (N'U'))
BEGIN
CREATE TABLE [{TABLEPREFIX}Gallery](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentGalleryId] [int] NULL,
	[Sequence] [int] NOT NULL,
	[CoverPhotoId] [int] NULL,
	[Slug] [nvarchar](max) COLLATE Latin1_General_CI_AS NOT NULL,
	[Title] [nvarchar](max) COLLATE Latin1_General_CI_AS NOT NULL,
	[Description] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_{TABLEPREFIX}Gallery] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[{TABLEPREFIX}PhotoTag]    Script Date: 07/08/2009 16:11:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{TABLEPREFIX}PhotoTag]') AND type in (N'U'))
BEGIN
CREATE TABLE [{TABLEPREFIX}PhotoTag](
	[PhotoId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
 CONSTRAINT [PK_{TABLEPREFIX}PhotoTag] PRIMARY KEY CLUSTERED 
(
	[PhotoId] ASC,
	[TagId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[{TABLEPREFIX}Page]    Script Date: 07/08/2009 16:11:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{TABLEPREFIX}Page]') AND type in (N'U'))
BEGIN
CREATE TABLE [{TABLEPREFIX}Page](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PhotoId] [int] NULL,
	[HidePhotoText] [bit] NOT NULL,
	[HidePhotoComments] [bit] NOT NULL,
	[ShowContactForm] [bit] NOT NULL,
	[Slug] [nvarchar](max) COLLATE Latin1_General_CI_AS NOT NULL,
	[Title] [nvarchar](max) COLLATE Latin1_General_CI_AS NOT NULL,
	[Text] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_{TABLEPREFIX}Page] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[{TABLEPREFIX}Comment]    Script Date: 07/08/2009 16:11:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{TABLEPREFIX}Comment]') AND type in (N'U'))
BEGIN
CREATE TABLE [{TABLEPREFIX}Comment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PhotoId] [int] NOT NULL,
	[ExternalId] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[Text] [nvarchar](max) COLLATE Latin1_General_CI_AS NOT NULL,
	[AuthorIsOwner] [bit] NOT NULL,
	[AuthorName] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[AuthorEmail] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[AuthorUrl] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[DatePublished] [datetime] NOT NULL,
 CONSTRAINT [PK_{TABLEPREFIX}Comment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[{TABLEPREFIX}GalleryTag]    Script Date: 07/08/2009 16:11:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{TABLEPREFIX}GalleryTag]') AND type in (N'U'))
BEGIN
CREATE TABLE [{TABLEPREFIX}GalleryTag](
	[GalleryId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
 CONSTRAINT [PK_{TABLEPREFIX}GalleryTag] PRIMARY KEY CLUSTERED 
(
	[GalleryId] ASC,
	[TagId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  ForeignKey [FK_{TABLEPREFIX}Comment_{TABLEPREFIX}Photo]    Script Date: 07/08/2009 16:11:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_{TABLEPREFIX}Comment_{TABLEPREFIX}Photo]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}Comment]'))
ALTER TABLE [{TABLEPREFIX}Comment]  WITH CHECK ADD  CONSTRAINT [FK_{TABLEPREFIX}Comment_{TABLEPREFIX}Photo] FOREIGN KEY([PhotoId])
REFERENCES [{TABLEPREFIX}Photo] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_{TABLEPREFIX}Comment_{TABLEPREFIX}Photo]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}Comment]'))
ALTER TABLE [{TABLEPREFIX}Comment] CHECK CONSTRAINT [FK_{TABLEPREFIX}Comment_{TABLEPREFIX}Photo]
GO
/****** Object:  ForeignKey [FK_{TABLEPREFIX}Gallery_{TABLEPREFIX}Photo]    Script Date: 07/08/2009 16:11:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_{TABLEPREFIX}Gallery_{TABLEPREFIX}Photo]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}Gallery]'))
ALTER TABLE [{TABLEPREFIX}Gallery]  WITH CHECK ADD  CONSTRAINT [FK_{TABLEPREFIX}Gallery_{TABLEPREFIX}Photo] FOREIGN KEY([CoverPhotoId])
REFERENCES [{TABLEPREFIX}Photo] ([Id])
ON DELETE SET NULL
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_{TABLEPREFIX}Gallery_{TABLEPREFIX}Photo]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}Gallery]'))
ALTER TABLE [{TABLEPREFIX}Gallery] CHECK CONSTRAINT [FK_{TABLEPREFIX}Gallery_{TABLEPREFIX}Photo]
GO
/****** Object:  ForeignKey [FK_ParentGallery_{TABLEPREFIX}Gallery]    Script Date: 09/24/2009 23:15:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ParentGallery_{TABLEPREFIX}Gallery]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}Gallery]'))
ALTER TABLE [{TABLEPREFIX}Gallery]  WITH CHECK ADD  CONSTRAINT [FK_ParentGallery_{TABLEPREFIX}Gallery] FOREIGN KEY([ParentGalleryId])
REFERENCES [{TABLEPREFIX}Gallery] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ParentGallery_{TABLEPREFIX}Gallery]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}Gallery]'))
ALTER TABLE [{TABLEPREFIX}Gallery] CHECK CONSTRAINT [FK_ParentGallery_{TABLEPREFIX}Gallery]
GO
/****** Object:  ForeignKey [FK_{TABLEPREFIX}GalleryTag_{TABLEPREFIX}Gallery]    Script Date: 07/08/2009 16:11:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_{TABLEPREFIX}GalleryTag_{TABLEPREFIX}Gallery]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}GalleryTag]'))
ALTER TABLE [{TABLEPREFIX}GalleryTag]  WITH CHECK ADD  CONSTRAINT [FK_{TABLEPREFIX}GalleryTag_{TABLEPREFIX}Gallery] FOREIGN KEY([GalleryId])
REFERENCES [{TABLEPREFIX}Gallery] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_{TABLEPREFIX}GalleryTag_{TABLEPREFIX}Gallery]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}GalleryTag]'))
ALTER TABLE [{TABLEPREFIX}GalleryTag] CHECK CONSTRAINT [FK_{TABLEPREFIX}GalleryTag_{TABLEPREFIX}Gallery]
GO
/****** Object:  ForeignKey [FK_{TABLEPREFIX}GalleryTag_{TABLEPREFIX}Tag]    Script Date: 07/08/2009 16:11:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_{TABLEPREFIX}GalleryTag_{TABLEPREFIX}Tag]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}GalleryTag]'))
ALTER TABLE [{TABLEPREFIX}GalleryTag]  WITH CHECK ADD  CONSTRAINT [FK_{TABLEPREFIX}GalleryTag_{TABLEPREFIX}Tag] FOREIGN KEY([TagId])
REFERENCES [{TABLEPREFIX}Tag] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_{TABLEPREFIX}GalleryTag_{TABLEPREFIX}Tag]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}GalleryTag]'))
ALTER TABLE [{TABLEPREFIX}GalleryTag] CHECK CONSTRAINT [FK_{TABLEPREFIX}GalleryTag_{TABLEPREFIX}Tag]
GO
/****** Object:  ForeignKey [FK_{TABLEPREFIX}Page_{TABLEPREFIX}Photo]    Script Date: 07/08/2009 16:11:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_{TABLEPREFIX}Page_{TABLEPREFIX}Photo]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}Page]'))
ALTER TABLE [{TABLEPREFIX}Page]  WITH CHECK ADD  CONSTRAINT [FK_{TABLEPREFIX}Page_{TABLEPREFIX}Photo] FOREIGN KEY([PhotoId])
REFERENCES [{TABLEPREFIX}Photo] ([Id])
ON DELETE SET NULL
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_{TABLEPREFIX}Page_{TABLEPREFIX}Photo]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}Page]'))
ALTER TABLE [{TABLEPREFIX}Page] CHECK CONSTRAINT [FK_{TABLEPREFIX}Page_{TABLEPREFIX}Photo]
GO
/****** Object:  ForeignKey [FK_{TABLEPREFIX}PhotoTag_{TABLEPREFIX}Photo]    Script Date: 07/08/2009 16:11:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_{TABLEPREFIX}PhotoTag_{TABLEPREFIX}Photo]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}PhotoTag]'))
ALTER TABLE [{TABLEPREFIX}PhotoTag]  WITH CHECK ADD  CONSTRAINT [FK_{TABLEPREFIX}PhotoTag_{TABLEPREFIX}Photo] FOREIGN KEY([PhotoId])
REFERENCES [{TABLEPREFIX}Photo] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_{TABLEPREFIX}PhotoTag_{TABLEPREFIX}Photo]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}PhotoTag]'))
ALTER TABLE [{TABLEPREFIX}PhotoTag] CHECK CONSTRAINT [FK_{TABLEPREFIX}PhotoTag_{TABLEPREFIX}Photo]
GO
/****** Object:  ForeignKey [FK_{TABLEPREFIX}PhotoTag_{TABLEPREFIX}Tag]    Script Date: 07/08/2009 16:11:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_{TABLEPREFIX}PhotoTag_{TABLEPREFIX}Tag]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}PhotoTag]'))
ALTER TABLE [{TABLEPREFIX}PhotoTag]  WITH CHECK ADD  CONSTRAINT [FK_{TABLEPREFIX}PhotoTag_{TABLEPREFIX}Tag] FOREIGN KEY([TagId])
REFERENCES [{TABLEPREFIX}Tag] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_{TABLEPREFIX}PhotoTag_{TABLEPREFIX}Tag]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}PhotoTag]'))
ALTER TABLE [{TABLEPREFIX}PhotoTag] CHECK CONSTRAINT [FK_{TABLEPREFIX}PhotoTag_{TABLEPREFIX}Tag]
GO
