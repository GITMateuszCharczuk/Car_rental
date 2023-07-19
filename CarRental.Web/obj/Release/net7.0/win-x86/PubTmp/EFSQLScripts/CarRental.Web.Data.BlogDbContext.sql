IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145250_blog')
BEGIN
    CREATE TABLE [BlogPosts] (
        [Id] uniqueidentifier NOT NULL,
        [Heading] nvarchar(max) NOT NULL,
        [PageTitle] nvarchar(max) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [ShortDescription] nvarchar(max) NOT NULL,
        [FeaturedImageUrl] nvarchar(max) NOT NULL,
        [UrlHandle] nvarchar(max) NOT NULL,
        [PublishedDate] datetime2 NOT NULL,
        [Author] nvarchar(max) NOT NULL,
        [Visible] bit NOT NULL,
        CONSTRAINT [PK_BlogPosts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145250_blog')
BEGIN
    CREATE TABLE [BlogPostComment] (
        [Id] uniqueidentifier NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [BlogPostId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [DateAdded] datetime2 NOT NULL,
        CONSTRAINT [PK_BlogPostComment] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BlogPostComment_BlogPosts_BlogPostId] FOREIGN KEY ([BlogPostId]) REFERENCES [BlogPosts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145250_blog')
BEGIN
    CREATE TABLE [BlogPostLike] (
        [Id] uniqueidentifier NOT NULL,
        [BlogPostId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_BlogPostLike] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BlogPostLike_BlogPosts_BlogPostId] FOREIGN KEY ([BlogPostId]) REFERENCES [BlogPosts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145250_blog')
BEGIN
    CREATE TABLE [Tags] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [BlogPostId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Tags] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Tags_BlogPosts_BlogPostId] FOREIGN KEY ([BlogPostId]) REFERENCES [BlogPosts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145250_blog')
BEGIN
    CREATE INDEX [IX_BlogPostComment_BlogPostId] ON [BlogPostComment] ([BlogPostId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145250_blog')
BEGIN
    CREATE INDEX [IX_BlogPostLike_BlogPostId] ON [BlogPostLike] ([BlogPostId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145250_blog')
BEGIN
    CREATE INDEX [IX_Tags_BlogPostId] ON [Tags] ([BlogPostId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145250_blog')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230626145250_blog', N'8.0.0-preview.2.23128.3');
END;
GO

COMMIT;
GO

