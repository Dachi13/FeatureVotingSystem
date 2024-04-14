CREATE PROCEDURE dbo.spGetRequestedFeaturesQuantity
	@ProductId INT,
	@FromDate DATETIME,
	@ToDate DATETIME
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @sql NVARCHAR(512);

	SET @sql = 'SELECT COUNT(f.Id) FROM Features f
                WHERE f.ProductId = @ProductId And f.StatusId != 3'

	IF @FromDate IS NOT NULL AND @ToDate IS NOT NULL
	BEGIN
		SET @sql = @sql + ' AND f.UploadDate BETWEEN @FromDate AND @ToDate'
	END

	EXEC sp_executesql @sql, N'@ProductId INT, @FromDate DATETIME, @ToDate DATETIME', @ProductId, @FromDate, @ToDate;
END
GO

CREATE PROCEDURE dbo.spGetRequestedFeaturesVotesQuantity
	@ProductId INT,
	@FromDate DATETIME,
	@ToDate DATETIME
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @sql NVARCHAR(1024);

	SET @sql = 'SELECT
					p.Name as ProductName,
					f.Name as FeatureName,
					SUM(CASE WHEN v.VoteValue = 1 THEN 1 ELSE 0 END) AS UpVotes,
					SUM(CASE WHEN v.VoteValue = -1 THEN 1 ELSE 0 END) AS DownVotes
                FROM Products p
                INNER JOIN Features f ON f.ProductId = p.Id
                LEFT JOIN Votes v ON v.FeatureId = f.Id
                WHERE p.Id = @ProductId AND p.IsDeleted = 0 AND f.StatusId != 3'

	IF @FromDate IS NOT NULL AND @ToDate IS NOT NULL
	BEGIN
		SET @sql = @sql + ' AND f.UploadDate BETWEEN @FromDate AND @ToDate'
	END

	SET @sql = @sql + CHAR(13) + CHAR(10) + 'GROUP BY p.Name, f.Name'

	EXEC sp_executesql @sql, N'@ProductId INT, @FromDate DATETIME, @ToDate DATETIME', @ProductId, @FromDate, @ToDate;
END
GO

CREATE PROCEDURE dbo.spGetRequestedFeaturesByVotesQuantityAndStatus
	@ProductId INT,
	@FromDate DATETIME,
	@ToDate DATETIME
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @sql NVARCHAR(1024);

	SET @sql = 'SELECT
	                f.Name AS FeatureName,
	                s.Name AS Status,
	                COUNT(v.VoteValue) AS VotesQuantity
                FROM Products p
                INNER JOIN Features f ON f.ProductId = p.Id
                LEFT JOIN Votes v ON v.FeatureId = f.Id
                INNER JOIN Statuses s ON s.Id = f.StatusId
                WHERE p.Id = @ProductId AND p.IsDeleted = 0 AND f.StatusId != 3'

	IF @FromDate IS NOT NULL AND @ToDate IS NOT NULL
	BEGIN
		SET @sql = @sql + ' AND f.UploadDate BETWEEN @FromDate AND @ToDate'
	END

	SET @sql = @sql + CHAR(13) + CHAR(10) + 'GROUP BY f.Name, s.Name' + CHAR(13) + CHAR(10) + 'ORDER BY VotesQuantity desc, s.Name'

	EXEC sp_executesql @sql, N'@ProductId INT, @FromDate DATETIME, @ToDate DATETIME', @ProductId, @FromDate, @ToDate;
END
GO

CREATE PROCEDURE dbo.spGetRequestedFeaturesByStatus
	@UserId INT,
	@FromDate DATETIME,
	@ToDate DATETIME
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @sql NVARCHAR(512);

	SET @sql = 'SELECT
	                f.Name AS FeatureName,
	                s.Name AS Status
                FROM Products p
                INNER JOIN Features f ON f.ProductId = p.Id
                INNER JOIN Statuses s ON s.Id = f.StatusId
                WHERE f.UserId = @UserId AND p.IsDeleted = 0 AND f.StatusId != 3'

	IF @FromDate IS NOT NULL AND @ToDate IS NOT NULL
	BEGIN
		SET @sql = @sql + ' AND f.UploadDate BETWEEN @FromDate AND @ToDate'
	END

	SET @sql = @sql + CHAR(13) + CHAR(10) + 'ORDER BY Status'

	EXEC sp_executesql @sql, N'@UserId INT, @FromDate DATETIME, @ToDate DATETIME', @UserId, @FromDate, @ToDate;
END
GO

CREATE PROCEDURE dbo.spGetUserRequestedFeaturesVotesQuantity
	@UserId INT,
	@FromDate DATETIME,
	@ToDate DATETIME
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @sql NVARCHAR(1024);

	SET @sql = 'SELECT
	                p.Name as ProductName,
	                f.Name as FeatureName,
	                SUM(CASE WHEN v.VoteValue = 1 THEN 1 ELSE 0 END) AS UpVotes,
	                SUM(CASE WHEN v.VoteValue = -1 THEN 1 ELSE 0 END) AS DownVotes
                FROM Products p
                INNER JOIN Features f ON f.ProductId = p.Id
                LEFT JOIN Votes v ON v.FeatureId = f.Id
                WHERE f.userId = @UserId AND p.IsDeleted = 0 AND f.StatusId != 3'

	IF @FromDate IS NOT NULL AND @ToDate IS NOT NULL
	BEGIN
		SET @sql = @sql + ' AND f.UploadDate BETWEEN @FromDate AND @ToDate'
	END

	SET @sql = @sql + CHAR(13) + CHAR(10) + 'GROUP BY p.Name, f.Name'

	EXEC sp_executesql @sql, N'@UserId INT, @FromDate DATETIME, @ToDate DATETIME', @UserId, @FromDate, @ToDate;
END
GO