
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/06/2013 17:25:40
-- Generated from EDMX file: D:\Projects\Projects\Geez\AllCode\Geez\Geez.Data\GeezModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Geez];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Checkin_ServiceProvider]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Checkin] DROP CONSTRAINT [FK_Checkin_ServiceProvider];
GO
IF OBJECT_ID(N'[dbo].[FK_Events_Events]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Events_Events];
GO
IF OBJECT_ID(N'[dbo].[FK_Menu_Application]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Menu] DROP CONSTRAINT [FK_Menu_Application];
GO
IF OBJECT_ID(N'[dbo].[FK_Menu_Menu]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Menu] DROP CONSTRAINT [FK_Menu_Menu];
GO
IF OBJECT_ID(N'[dbo].[FK_Recommendation_ServiceProvider]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Recommendation] DROP CONSTRAINT [FK_Recommendation_ServiceProvider];
GO
IF OBJECT_ID(N'[dbo].[FK_ServiceProvider_Address]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceProvider] DROP CONSTRAINT [FK_ServiceProvider_Address];
GO
IF OBJECT_ID(N'[dbo].[FK_ServiceProvider_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceProvider] DROP CONSTRAINT [FK_ServiceProvider_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_ServiceProviderPicture_ServiceProvider]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceProviderPicture] DROP CONSTRAINT [FK_ServiceProviderPicture_ServiceProvider];
GO
IF OBJECT_ID(N'[dbo].[FK_ServiceProviderRating_Service]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceProviderRating] DROP CONSTRAINT [FK_ServiceProviderRating_Service];
GO
IF OBJECT_ID(N'[dbo].[FK_ServiceProviderRating_ServiceProvider]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceProviderRating] DROP CONSTRAINT [FK_ServiceProviderRating_ServiceProvider];
GO
IF OBJECT_ID(N'[dbo].[FK_ServiceProviderService_Service]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceProviderService] DROP CONSTRAINT [FK_ServiceProviderService_Service];
GO
IF OBJECT_ID(N'[dbo].[FK_ServiceProviderService_ServiceProvider]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceProviderService] DROP CONSTRAINT [FK_ServiceProviderService_ServiceProvider];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Address]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Address];
GO
IF OBJECT_ID(N'[dbo].[Application]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Application];
GO
IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[Checkin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Checkin];
GO
IF OBJECT_ID(N'[dbo].[Event]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Event];
GO
IF OBJECT_ID(N'[dbo].[Fault]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fault];
GO
IF OBJECT_ID(N'[dbo].[Menu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Menu];
GO
IF OBJECT_ID(N'[dbo].[Message]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Message];
GO
IF OBJECT_ID(N'[dbo].[Rating]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rating];
GO
IF OBJECT_ID(N'[dbo].[Recommendation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Recommendation];
GO
IF OBJECT_ID(N'[dbo].[Request]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Request];
GO
IF OBJECT_ID(N'[dbo].[Response]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Response];
GO
IF OBJECT_ID(N'[dbo].[Service]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Service];
GO
IF OBJECT_ID(N'[dbo].[ServiceProvider]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServiceProvider];
GO
IF OBJECT_ID(N'[dbo].[ServiceProviderPicture]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServiceProviderPicture];
GO
IF OBJECT_ID(N'[dbo].[ServiceProviderRating]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServiceProviderRating];
GO
IF OBJECT_ID(N'[dbo].[ServiceProviderService]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServiceProviderService];
GO
IF OBJECT_ID(N'[dbo].[Setting]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Setting];
GO
IF OBJECT_ID(N'[dbo].[TransactionLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionLog];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Address'
CREATE TABLE [dbo].[Address] (
    [Id] bigint  NOT NULL,
    [Region] nvarchar(50)  NULL,
    [City] nvarchar(50)  NULL,
    [Zone] nvarchar(50)  NULL,
    [Street] nvarchar(50)  NULL,
    [Details] nvarchar(max)  NULL
);
GO

-- Creating table 'Application'
CREATE TABLE [dbo].[Application] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FileName] nvarchar(50)  NOT NULL,
    [AssemblyName] nvarchar(50)  NOT NULL,
    [ClassName] nvarchar(50)  NOT NULL,
    [Description] nvarchar(250)  NULL,
    [ServiceUrl] nvarchar(250)  NULL
);
GO

-- Creating table 'Category'
CREATE TABLE [dbo].[Category] (
    [Id] int  NOT NULL,
    [Description] nvarchar(150)  NOT NULL,
    [Image] nvarchar(150)  NULL
);
GO

-- Creating table 'Checkin'
CREATE TABLE [dbo].[Checkin] (
    [Id] bigint  NOT NULL,
    [PhoneNumber] nvarchar(50)  NOT NULL,
    [DateTime] datetime  NOT NULL,
    [ServiceProviderId] int  NOT NULL
);
GO

-- Creating table 'Fault'
CREATE TABLE [dbo].[Fault] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [TransactionId] nvarchar(50)  NOT NULL,
    [TransactionTime] datetime  NOT NULL,
    [FaultCode] nvarchar(50)  NOT NULL,
    [FaultString] nvarchar(500)  NOT NULL
);
GO

-- Creating table 'Menu'
CREATE TABLE [dbo].[Menu] (
    [Id] int  NOT NULL,
    [ParentId] int  NOT NULL,
    [ApplicationId] int  NOT NULL,
    [Title] nvarchar(50)  NOT NULL,
    [MethodName] nvarchar(50)  NOT NULL,
    [State] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Rating'
CREATE TABLE [dbo].[Rating] (
    [Id] int  NOT NULL,
    [Description] nvarchar(50)  NOT NULL,
    [Value] int  NOT NULL
);
GO

-- Creating table 'Request'
CREATE TABLE [dbo].[Request] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [TransactionId] nvarchar(50)  NOT NULL,
    [TransactionTime] datetime  NOT NULL,
    [MSISDN] nvarchar(50)  NOT NULL,
    [USSDServiceCode] nvarchar(50)  NOT NULL,
    [USSDRequestString] nvarchar(300)  NOT NULL,
    [Response] bit  NULL,
    [ChargingFlag] bit  NULL,
    [ChargeCode] decimal(18,0)  NULL
);
GO

-- Creating table 'Response'
CREATE TABLE [dbo].[Response] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [TransactionId] nvarchar(50)  NOT NULL,
    [USSDResponseString] nvarchar(500)  NOT NULL,
    [Action] nvarchar(50)  NULL,
    [ResponseCode] int  NULL,
    [TransactionTime] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Service'
CREATE TABLE [dbo].[Service] (
    [Id] int  NOT NULL,
    [ServiceName] nvarchar(max)  NOT NULL,
    [Image] nvarchar(150)  NULL
);
GO

-- Creating table 'ServiceProvider'
CREATE TABLE [dbo].[ServiceProvider] (
    [Id] int  NOT NULL,
    [CategoryId] int  NOT NULL,
    [AddressId] bigint  NOT NULL,
    [OrganizationName] nvarchar(max)  NOT NULL,
    [OpeningDays] nvarchar(max)  NOT NULL,
    [Details] nvarchar(max)  NULL
);
GO

-- Creating table 'ServiceProviderPicture'
CREATE TABLE [dbo].[ServiceProviderPicture] (
    [Id] bigint  NOT NULL,
    [ServiceProviderId] int  NOT NULL,
    [Image] nvarchar(150)  NOT NULL,
    [Description] nvarchar(250)  NULL,
    [Title] nvarchar(50)  NULL
);
GO

-- Creating table 'ServiceProviderRating'
CREATE TABLE [dbo].[ServiceProviderRating] (
    [Id] bigint  NOT NULL,
    [ServiceProviderId] int  NOT NULL,
    [ServiceId] int  NOT NULL,
    [AverageRating] decimal(10,2)  NOT NULL,
    [RatingCount] int  NOT NULL
);
GO

-- Creating table 'Setting'
CREATE TABLE [dbo].[Setting] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SettingKey] nvarchar(50)  NOT NULL,
    [SettingValue] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Event'
CREATE TABLE [dbo].[Event] (
    [Id] bigint  NOT NULL,
    [ServiceProviderId] int  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Details] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [Role] bit  NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [MobileNumber] nvarchar(50)  NOT NULL,
    [DateRegistered] datetime  NOT NULL
);
GO

-- Creating table 'TransactionLog'
CREATE TABLE [dbo].[TransactionLog] (
    [Id] bigint  NOT NULL,
    [TransactionId] nvarchar(50)  NOT NULL,
    [State] nchar(10)  NOT NULL,
    [MenuId] bigint  NOT NULL,
    [LoggedMenu] tinyint  NOT NULL
);
GO

-- Creating table 'Recommendation'
CREATE TABLE [dbo].[Recommendation] (
    [Id] bigint  NOT NULL,
    [RecommendedBy] nvarchar(50)  NOT NULL,
    [RecommendedTo] nvarchar(50)  NOT NULL,
    [ServiceProviderId] int  NOT NULL,
    [RecommendedOn] datetime  NOT NULL,
    [Status] tinyint  NOT NULL
);
GO

-- Creating table 'Message'
CREATE TABLE [dbo].[Message] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [FullName] nvarchar(300)  NOT NULL,
    [MobileNumber] nchar(20)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Subject] nvarchar(200)  NOT NULL,
    [Body] varchar(max)  NOT NULL,
    [SentOn] datetime  NOT NULL,
    [Status] tinyint  NOT NULL
);
GO

-- Creating table 'ServiceProviderService1'
CREATE TABLE [dbo].[ServiceProviderService1] (
    [Service_Id] int  NOT NULL,
    [ServiceProvider_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Address'
ALTER TABLE [dbo].[Address]
ADD CONSTRAINT [PK_Address]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Application'
ALTER TABLE [dbo].[Application]
ADD CONSTRAINT [PK_Application]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Category'
ALTER TABLE [dbo].[Category]
ADD CONSTRAINT [PK_Category]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Checkin'
ALTER TABLE [dbo].[Checkin]
ADD CONSTRAINT [PK_Checkin]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Fault'
ALTER TABLE [dbo].[Fault]
ADD CONSTRAINT [PK_Fault]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Menu'
ALTER TABLE [dbo].[Menu]
ADD CONSTRAINT [PK_Menu]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Rating'
ALTER TABLE [dbo].[Rating]
ADD CONSTRAINT [PK_Rating]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Request'
ALTER TABLE [dbo].[Request]
ADD CONSTRAINT [PK_Request]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Response'
ALTER TABLE [dbo].[Response]
ADD CONSTRAINT [PK_Response]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Service'
ALTER TABLE [dbo].[Service]
ADD CONSTRAINT [PK_Service]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ServiceProvider'
ALTER TABLE [dbo].[ServiceProvider]
ADD CONSTRAINT [PK_ServiceProvider]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ServiceProviderPicture'
ALTER TABLE [dbo].[ServiceProviderPicture]
ADD CONSTRAINT [PK_ServiceProviderPicture]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ServiceProviderRating'
ALTER TABLE [dbo].[ServiceProviderRating]
ADD CONSTRAINT [PK_ServiceProviderRating]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Setting'
ALTER TABLE [dbo].[Setting]
ADD CONSTRAINT [PK_Setting]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [PK_Event]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TransactionLog'
ALTER TABLE [dbo].[TransactionLog]
ADD CONSTRAINT [PK_TransactionLog]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Recommendation'
ALTER TABLE [dbo].[Recommendation]
ADD CONSTRAINT [PK_Recommendation]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Message'
ALTER TABLE [dbo].[Message]
ADD CONSTRAINT [PK_Message]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Service_Id], [ServiceProvider_Id] in table 'ServiceProviderService1'
ALTER TABLE [dbo].[ServiceProviderService1]
ADD CONSTRAINT [PK_ServiceProviderService1]
    PRIMARY KEY NONCLUSTERED ([Service_Id], [ServiceProvider_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AddressId] in table 'ServiceProvider'
ALTER TABLE [dbo].[ServiceProvider]
ADD CONSTRAINT [FK_ServiceProvider_Address]
    FOREIGN KEY ([AddressId])
    REFERENCES [dbo].[Address]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ServiceProvider_Address'
CREATE INDEX [IX_FK_ServiceProvider_Address]
ON [dbo].[ServiceProvider]
    ([AddressId]);
GO

-- Creating foreign key on [CategoryId] in table 'ServiceProvider'
ALTER TABLE [dbo].[ServiceProvider]
ADD CONSTRAINT [FK_ServiceProvider_Category]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Category]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ServiceProvider_Category'
CREATE INDEX [IX_FK_ServiceProvider_Category]
ON [dbo].[ServiceProvider]
    ([CategoryId]);
GO

-- Creating foreign key on [ServiceProviderId] in table 'Checkin'
ALTER TABLE [dbo].[Checkin]
ADD CONSTRAINT [FK_Checkin_ServiceProvider]
    FOREIGN KEY ([ServiceProviderId])
    REFERENCES [dbo].[ServiceProvider]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Checkin_ServiceProvider'
CREATE INDEX [IX_FK_Checkin_ServiceProvider]
ON [dbo].[Checkin]
    ([ServiceProviderId]);
GO

-- Creating foreign key on [ParentId] in table 'Menu'
ALTER TABLE [dbo].[Menu]
ADD CONSTRAINT [FK_Menu_Menu]
    FOREIGN KEY ([ParentId])
    REFERENCES [dbo].[Menu]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Menu_Menu'
CREATE INDEX [IX_FK_Menu_Menu]
ON [dbo].[Menu]
    ([ParentId]);
GO

-- Creating foreign key on [ServiceId] in table 'ServiceProviderRating'
ALTER TABLE [dbo].[ServiceProviderRating]
ADD CONSTRAINT [FK_ServiceProviderRating_Service]
    FOREIGN KEY ([ServiceId])
    REFERENCES [dbo].[Service]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ServiceProviderRating_Service'
CREATE INDEX [IX_FK_ServiceProviderRating_Service]
ON [dbo].[ServiceProviderRating]
    ([ServiceId]);
GO

-- Creating foreign key on [ServiceProviderId] in table 'ServiceProviderPicture'
ALTER TABLE [dbo].[ServiceProviderPicture]
ADD CONSTRAINT [FK_ServiceProviderPicture_ServiceProvider]
    FOREIGN KEY ([ServiceProviderId])
    REFERENCES [dbo].[ServiceProvider]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ServiceProviderPicture_ServiceProvider'
CREATE INDEX [IX_FK_ServiceProviderPicture_ServiceProvider]
ON [dbo].[ServiceProviderPicture]
    ([ServiceProviderId]);
GO

-- Creating foreign key on [ServiceProviderId] in table 'ServiceProviderRating'
ALTER TABLE [dbo].[ServiceProviderRating]
ADD CONSTRAINT [FK_ServiceProviderRating_ServiceProvider]
    FOREIGN KEY ([ServiceProviderId])
    REFERENCES [dbo].[ServiceProvider]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ServiceProviderRating_ServiceProvider'
CREATE INDEX [IX_FK_ServiceProviderRating_ServiceProvider]
ON [dbo].[ServiceProviderRating]
    ([ServiceProviderId]);
GO

-- Creating foreign key on [Service_Id] in table 'ServiceProviderService1'
ALTER TABLE [dbo].[ServiceProviderService1]
ADD CONSTRAINT [FK_ServiceProviderService1_Service]
    FOREIGN KEY ([Service_Id])
    REFERENCES [dbo].[Service]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ServiceProvider_Id] in table 'ServiceProviderService1'
ALTER TABLE [dbo].[ServiceProviderService1]
ADD CONSTRAINT [FK_ServiceProviderService1_ServiceProvider]
    FOREIGN KEY ([ServiceProvider_Id])
    REFERENCES [dbo].[ServiceProvider]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ServiceProviderService1_ServiceProvider'
CREATE INDEX [IX_FK_ServiceProviderService1_ServiceProvider]
ON [dbo].[ServiceProviderService1]
    ([ServiceProvider_Id]);
GO

-- Creating foreign key on [ServiceProviderId] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [FK_Events_Events]
    FOREIGN KEY ([ServiceProviderId])
    REFERENCES [dbo].[ServiceProvider]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Events_Events'
CREATE INDEX [IX_FK_Events_Events]
ON [dbo].[Event]
    ([ServiceProviderId]);
GO

-- Creating foreign key on [ServiceProviderId] in table 'Recommendation'
ALTER TABLE [dbo].[Recommendation]
ADD CONSTRAINT [FK_Recommendation_ServiceProvider]
    FOREIGN KEY ([ServiceProviderId])
    REFERENCES [dbo].[ServiceProvider]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Recommendation_ServiceProvider'
CREATE INDEX [IX_FK_Recommendation_ServiceProvider]
ON [dbo].[Recommendation]
    ([ServiceProviderId]);
GO

-- Creating foreign key on [ApplicationId] in table 'Menu'
ALTER TABLE [dbo].[Menu]
ADD CONSTRAINT [FK_Menu_Application]
    FOREIGN KEY ([ApplicationId])
    REFERENCES [dbo].[Application]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Menu_Application'
CREATE INDEX [IX_FK_Menu_Application]
ON [dbo].[Menu]
    ([ApplicationId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------