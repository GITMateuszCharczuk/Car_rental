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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145231_car')
BEGIN
    CREATE TABLE [CarOffers] (
        [Id] uniqueidentifier NOT NULL,
        [Heading] nvarchar(max) NOT NULL,
        [ShortDescription] nvarchar(max) NOT NULL,
        [FeaturedImageUrl] nvarchar(max) NOT NULL,
        [UrlHandle] nvarchar(max) NOT NULL,
        [Horsepower] nvarchar(max) NOT NULL,
        [YearOfProduction] int NOT NULL,
        [EngineDetails] nvarchar(max) NOT NULL,
        [DriveDetails] nvarchar(max) NOT NULL,
        [GearboxDetails] nvarchar(max) NOT NULL,
        [CarDeliverylocation] nvarchar(max) NOT NULL,
        [CarReturnLocation] nvarchar(max) NOT NULL,
        [PublishedDate] datetime2 NOT NULL,
        [Visible] bit NOT NULL,
        CONSTRAINT [PK_CarOffers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145231_car')
BEGIN
    CREATE TABLE [CarOrders] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [CarOfferId] uniqueidentifier NOT NULL,
        [UserName] nvarchar(max) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [NumOfDrivers] int NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Surname] nvarchar(max) NOT NULL,
        [PhoneNumber] int NOT NULL,
        [EmailAddress] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [Postcode] nvarchar(max) NOT NULL,
        [City] nvarchar(max) NOT NULL,
        [DriversLicenseNumber] nvarchar(max) NOT NULL,
        [TotalPrice] float NOT NULL,
        [State] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_CarOrders] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145231_car')
BEGIN
    CREATE TABLE [CarTags] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [CarOfferId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_CarTags] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CarTags_CarOffers_CarOfferId] FOREIGN KEY ([CarOfferId]) REFERENCES [CarOffers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145231_car')
BEGIN
    CREATE TABLE [ImageUrls] (
        [Id] uniqueidentifier NOT NULL,
        [CarOfferId] uniqueidentifier NOT NULL,
        [Url] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ImageUrls] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ImageUrls_CarOffers_CarOfferId] FOREIGN KEY ([CarOfferId]) REFERENCES [CarOffers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145231_car')
BEGIN
    CREATE TABLE [Tarrifs] (
        [Id] uniqueidentifier NOT NULL,
        [CarOfferId] uniqueidentifier NOT NULL,
        [OneNormalDayPrice] float NOT NULL,
        [OneWeekendDayPrice] float NOT NULL,
        [FullWeekendPrice] float NOT NULL,
        [OneWeekPrice] float NOT NULL,
        [OneMonthPrice] float NOT NULL,
        CONSTRAINT [PK_Tarrifs] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Tarrifs_CarOffers_CarOfferId] FOREIGN KEY ([CarOfferId]) REFERENCES [CarOffers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145231_car')
BEGIN
    CREATE INDEX [IX_CarTags_CarOfferId] ON [CarTags] ([CarOfferId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145231_car')
BEGIN
    CREATE INDEX [IX_ImageUrls_CarOfferId] ON [ImageUrls] ([CarOfferId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145231_car')
BEGIN
    CREATE UNIQUE INDEX [IX_Tarrifs_CarOfferId] ON [Tarrifs] ([CarOfferId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230626145231_car')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230626145231_car', N'8.0.0-preview.2.23128.3');
END;
GO

COMMIT;
GO

