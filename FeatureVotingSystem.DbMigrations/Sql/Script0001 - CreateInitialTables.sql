create table Products (
	Id int primary key identity(1,1),
	[Name] nvarchar(30) not null,
	ShortDesc nvarchar(256) not null,
	UserId int not null,
	CreatedAt datetime not null,
	IsDeleted bit not null default 0,
	foreign key (UserId) references Users(Id)
)

create table Statuses (
	Id int primary key identity(1,1),
	[Name] varchar(20) not null check ([Name] in ('Active', 'In Progress', 'Deleted', 'Rejected', 'Completed')),
)

insert into Statuses ([Name])
values ('Active'),
	('In Progress'),
	('Deleted'),
	('Rejected'),
	('Completed');

create table Features (
	Id int primary key identity(1,1), 
	[Name] nvarchar(50) not null,
	[Description] nvarchar(512) not null,
	RejectionReason nvarchar(256),
	UserId int not null,
	ProductId int not null,
	StatusId int not null DEFAULT 1,
	foreign key (UserId) references Users(Id),
	foreign key (ProductId) references Products(Id),
	foreign key (StatusId) references Statuses(Id)
)

create table Comments (
	Id int primary key identity(1,1),
	CreatedAt datetime not null,
	UserId int not null,
	FeatureId int not null,
	[Text] nvarchar(512) not null,
	IsDeleted bit not null default 0,
	foreign key (UserId) references Users(Id),
	foreign key (FeatureId) references Features(Id)
)

ALTER TABLE Users
ADD CONSTRAINT UK_NormalizedEmail UNIQUE (NormalizedEmail)

ALTER TABLE Users
ADD IsDeleted bit NOT NULL DEFAULT 0;

CREATE TABLE [dbo].[Votes] (
    [UserId]    INT      NOT NULL,
    [FeatureId] INT      NOT NULL,
    [ProductId] INT      NOT NULL,
    [VoteValue] smallint  NOT NULL,
    [VotedAt]   DATETIME NOT NULL,
     IsDeleted bit not null default 0,
     CONSTRAINT [PK_Votes] PRIMARY KEY CLUSTERED ([UserId] ASC, [FeatureId] ASC),
    CONSTRAINT [FK_Votes_Feature] FOREIGN KEY ([FeatureId]) REFERENCES [dbo].[Features] ([Id]),
    CONSTRAINT [FK_Votes_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]),
    CONSTRAINT [FK_Votes_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
    );
GO
   
CREATE NONCLUSTERED INDEX [VotesByUserProductDate]
    ON [dbo].[Votes]([UserId] ASC, [ProductId] ASC, [VotedAt] ASC);
GO

ALTER TABLE [dbo].[Features]
    ADD [UploadDate] DATETIME NOT NULL;
