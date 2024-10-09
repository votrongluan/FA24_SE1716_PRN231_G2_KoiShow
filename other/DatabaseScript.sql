USE master;
GO
DROP DATABASE IF EXISTS FA24_SE1716_PRN231_G2_KoiShow;
GO
CREATE DATABASE FA24_SE1716_PRN231_G2_KoiShow;
GO
USE FA24_SE1716_PRN231_G2_KoiShow;

CREATE TABLE [Contest] (
	[ContestId] int IDENTITY(1,1) NOT NULL UNIQUE,
	[ContestName] nvarchar(MAX) NOT NULL,
	[Title] nvarchar(MAX) NOT NULL,
	[Description] nvarchar(MAX) NOT NULL,
	[StartDate] datetime NOT NULL,
	[EndDate] datetime NOT NULL,
	[Location] nvarchar(MAX) NOT NULL,
	[CompetitionType] nvarchar(max) NOT NULL,
	[Status] int NOT NULL,
	[Participants] int NOT NULL,
	[Image] nvarchar(MAX) NOT NULL,
	[ContactInfo] nvarchar(MAX) NOT NULL,
	[ShapePointPercent] int NOT NULL,
	[ColorPointPercent] int NOT NULL,
	[PatternPointPercent] int NOT NULL,
	PRIMARY KEY ([ContestId])
);

CREATE TABLE [New] (
	[NewId] int IDENTITY(1,1) NOT NULL UNIQUE,
	[Title] nvarchar(MAX) NOT NULL,
	[Description] nvarchar(MAX) NOT NULL,
	[HtmlContent] nvarchar(MAX) NOT NULL,
	[Category] nvarchar(MAX) NOT NULL,
	[Tags] nvarchar(MAX) NOT NULL,
	[IsPublished] nvarchar(max) NOT NULL,
	[PublishDate] datetime NOT NULL,
	[FeaturedImage] nvarchar(MAX) NOT NULL,
	[ViewCount] int NOT NULL,
	[AuthorId] int NOT NULL,
	PRIMARY KEY ([NewId])
);

CREATE TABLE [Animal] (
	[AnimalId] int IDENTITY(1,1) NOT NULL UNIQUE,
	[AnimalName] nvarchar(MAX) NOT NULL,
	[VarietyId] int NOT NULL,
	[Size] int NOT NULL,
	[BirthDate] datetime NOT NULL,
	[ImgLink] nvarchar(MAX) NOT NULL,
	[OwnerId] int NOT NULL,
	[Weight] int NOT NULL,
	[Description] nvarchar(MAX) NOT NULL,
	[HeathStatus] nvarchar(MAX) NOT NULL,
	[Gender] int NOT NULL,
	PRIMARY KEY ([AnimalId])
);

CREATE TABLE [Variety] (
	[VarietyId] int IDENTITY(1,1) NOT NULL UNIQUE,
	[Description] nvarchar(MAX) NOT NULL,
	[Name] nvarchar(MAX) NOT NULL,
	PRIMARY KEY ([VarietyId])
);

CREATE TABLE [Point] (
	[PointId] int IDENTITY(1,1) NOT NULL UNIQUE,
	[ShapePoint] int,
	[ColorPoint] int,
	[PatternPoint] int,
	[Comment] nvarchar(MAX),
	[JuryId] int NOT NULL,
	[RegisterFormId] int NOT NULL,
	[PointStatus] int NOT NULL,
	[JudgeRank] nvarchar(MAX) NOT NULL,
	[Penalties] int NOT NULL,
	[TotalScore] int NOT NULL,
	PRIMARY KEY ([PointId])
);

CREATE TABLE [Account] (
	[AccountId] int IDENTITY(1,1) NOT NULL UNIQUE,
	[UserName] nvarchar(MAX) NOT NULL,
	[Password] nvarchar(MAX) NOT NULL,
	[Email] nvarchar(MAX) NOT NULL,
	[FullName] nvarchar(MAX) NOT NULL,
	[Role] int NOT NULL,
	[Phone] nvarchar(15) NOT NULL,
	[Address] nvarchar(Max) NOT NULL,
	[Status] int NOT NULL,
	[BirthDay] date NOT NULL,
	PRIMARY KEY ([AccountId])
);

CREATE TABLE [RegisterForm] (
	[RegisterFormId] int IDENTITY(1,1) NOT NULL UNIQUE,
	[ContestId] int NOT NULL,
	[AnimalId] int NOT NULL,
	[EntryTitle] nvarchar(MAX) NOT NULL,
	[CheckinStatus] nvarchar(max) NOT NULL,
	[RegistrationDate] datetime NOT NULL,
	[ApprovalStatus] nvarchar(max) NOT NULL,
	[Notes] nvarchar(MAX) NOT NULL,
	[Status] nvarchar(max) NOT NULL,
	[Image] nvarchar(MAX) NOT NULL,
	[HealthStatus] nvarchar(MAX) NOT NULL,
	[Color] nvarchar(MAX) NOT NULL,
	[Shape] nvarchar(MAX) NOT NULL,
	[Pattern] nvarchar(MAX) NOT NULL,
	PRIMARY KEY ([RegisterFormId])
);

CREATE TABLE [ContestResult] (
	[ContestResultId] int IDENTITY(1,1) NOT NULL UNIQUE,
	[ContestId] int NOT NULL,
	[ContestResultName] nvarchar(MAX) NOT NULL,
	[ContestResultDescription] nvarchar(MAX) NOT NULL,
	[TotalScore] float(53) NOT NULL,
	[Rank] int NOT NULL,
	[Comments] nvarchar(MAX) NOT NULL,
	[IsFinalized] nvarchar(max) NOT NULL,
	[IsPublished] nvarchar(max) NOT NULL,
	[Category] nvarchar(MAX) NOT NULL,
	[Status] nvarchar(max) NOT NULL,
	[Prize] int NOT NULL,
	[PrizeDescription] nvarchar(MAX) NOT NULL,
	PRIMARY KEY ([ContestResultId])
);

CREATE TABLE [Payment] (
	[PaymentId] int IDENTITY(1,1) NOT NULL UNIQUE,
	[RegisterFormId] int NOT NULL,
	[TransactionId] nvarchar(max) NOT NULL,
	[PaymentAmount] float(53) NOT NULL,
	[PaymentDate] datetime NOT NULL,
	[PaymentStatus] nvarchar(max) NOT NULL,
	[Description] nvarchar(MAX) NOT NULL,
	[VATRate] float(53) NOT NULL,
	[ActualCost] float(53) NOT NULL,
	[DiscountAmount] float(53) NOT NULL,
	[FinalAmount] float(53) NOT NULL,
	[Currency] nvarchar(MAX) NOT NULL,
	PRIMARY KEY ([PaymentId])
);


ALTER TABLE [New] ADD CONSTRAINT [New_fk10] FOREIGN KEY ([AuthorId]) REFERENCES [Account]([AccountId]);
ALTER TABLE [Animal] ADD CONSTRAINT [Animal_fk2] FOREIGN KEY ([VarietyId]) REFERENCES [Variety]([VarietyId]);

ALTER TABLE [Animal] ADD CONSTRAINT [Animal_fk6] FOREIGN KEY ([OwnerId]) REFERENCES [Account]([AccountId]);

ALTER TABLE [Point] ADD CONSTRAINT [Point_fk5] FOREIGN KEY ([JuryId]) REFERENCES [Account]([AccountId]);

ALTER TABLE [Point] ADD CONSTRAINT [Point_fk6] FOREIGN KEY ([RegisterFormId]) REFERENCES [RegisterForm]([RegisterFormId]);

ALTER TABLE [RegisterForm] ADD CONSTRAINT [RegisterForm_fk1] FOREIGN KEY ([ContestId]) REFERENCES [Contest]([ContestId]);

ALTER TABLE [RegisterForm] ADD CONSTRAINT [RegisterForm_fk2] FOREIGN KEY ([AnimalId]) REFERENCES [Animal]([AnimalId]);
ALTER TABLE [ContestResult] ADD CONSTRAINT [ContestResult_fk1] FOREIGN KEY ([ContestId]) REFERENCES [Contest]([ContestId]);
ALTER TABLE [Payment] ADD CONSTRAINT [Payment_fk1] FOREIGN KEY ([RegisterFormId]) REFERENCES [RegisterForm]([RegisterFormId]);