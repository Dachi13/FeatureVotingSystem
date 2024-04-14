CREATE PROCEDURE dbo.spGetEmailQueue
AS
BEGIN
	SELECT e.Id,
			u.Name AS UserName, 
			u.Email,
			es.SubjectName,
			e.EmailText
	FROM EmailQueue AS e
	INNER JOIN Users u ON e.UserId = u.Id
	INNER JOIN EmailSubjects es ON e.EmailSubjectId = es.Id
	WHERE e.IsSent = 0
END
GO

CREATE PROCEDURE dbo.spMarkEmailAsSent
	@EmailQueueId INT
AS
BEGIN
	UPDATE EmailQueue SET IsSent = 1 WHERE Id = @EmailQueueId
END
GO
