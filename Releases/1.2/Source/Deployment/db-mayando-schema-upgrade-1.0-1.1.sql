/* Before executing this script:
    - Replace {DBNAME} with the name of your database (e.g. Mayando).
    - Replace {TABLEPREFIX} with the optional (unique) prefix for all database
      tables; use when multiple application instances are installed in the same
      database. Remove the {TABLEPREFIX} text everywhere if you don't need a table
      prefix.
*/

USE {DBNAME}

/****** Add "Hidden" column to "Photo" table ******/
ALTER TABLE [{TABLEPREFIX}Photo]
ADD [Hidden] [bit] NOT NULL DEFAULT 0
GO

/****** Add [ParentGalleryId] column to "Gallery" table ******/
ALTER TABLE [{TABLEPREFIX}Gallery]
ADD [ParentGalleryId] [int] NULL
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ParentGallery_{TABLEPREFIX}Gallery]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}Gallery]'))
ALTER TABLE [{TABLEPREFIX}Gallery]  WITH CHECK ADD  CONSTRAINT [FK_ParentGallery_{TABLEPREFIX}Gallery] FOREIGN KEY([ParentGalleryId])
REFERENCES [{TABLEPREFIX}Gallery] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ParentGallery_{TABLEPREFIX}Gallery]') AND parent_object_id = OBJECT_ID(N'[{TABLEPREFIX}Gallery]'))
ALTER TABLE [{TABLEPREFIX}Gallery] CHECK CONSTRAINT [FK_ParentGallery_{TABLEPREFIX}Gallery]
GO