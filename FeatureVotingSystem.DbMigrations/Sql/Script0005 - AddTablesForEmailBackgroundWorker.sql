CREATE TABLE EmailSubjects (
	Id int primary key identity(1,1),
	SubjectName nvarchar(70) not null
);

INSERT INTO EmailSubjects (SubjectName)
VALUES ('Feature Request'),
		('Feature Description change'),
		('Feature status was changed'),
		('Feature vote'),
		('Feature comment');

CREATE TABLE EmailQueue (
	Id int primary key identity(1,1),
	UserId int not null,
	EmailSubjectId int not null,
	EmailText nvarchar(256) not null,
	IsSent bit default(0) not null,
	FOREIGN KEY (UserId) REFERENCES Users(Id),
	FOREIGN KEY (EmailSubjectId) REFERENCES EmailSubjects(Id)
);