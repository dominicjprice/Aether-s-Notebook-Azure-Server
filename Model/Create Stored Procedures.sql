SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('dbo.InsertLocation') IS NOT NULL
DROP PROCEDURE [dbo].[InsertLocation]
GO

CREATE PROCEDURE [dbo].[InsertLocation]
	@ID bigint OUTPUT,
	@accuracy float,
	@altitude float,
	@bearing float,
	@extras text,
	@provider varchar(MAX),
	@speed float,
	@time bigint,
	@latitude float,
	@longitude float
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Location (accuracy, altitude, bearing, extras, provider, speed, [time], longitude, latitude, position)
	VALUES (@accuracy, @altitude, @bearing, @extras, @provider, @speed, @time, @longitude, @latitude, Geography::Point(@latitude, @longitude, 4326))
	SET @ID = CAST(SCOPE_IDENTITY() AS bigint)
END
GO

IF OBJECT_ID('dbo.UpdateLocation') IS NOT NULL
DROP PROCEDURE [dbo].[UpdateLocation]
GO

CREATE PROCEDURE [dbo].[UpdateLocation]
	@ID bigint,
	@accuracy float,
	@altitude float,
	@bearing float,
	@extras text,
	@provider varchar(MAX),
	@speed float,
	@time bigint,
	@latitude float,
	@longitude float
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Location SET accuracy = @accuracy, altitude = @altitude, bearing = @bearing, extras = @extras, provider = @provider,
		speed = @speed, [time] = @time, longitude = @longitude, latitude = @latitude, position = Geography::Point(@latitude, @longitude, 4326)
	WHERE ID = @ID
END
GO

IF OBJECT_ID('dbo.InsertPositionLocationChanged') IS NOT NULL
DROP PROCEDURE [dbo].InsertPositionLocationChanged
GO

CREATE PROCEDURE [dbo].[InsertPositionLocationChanged]
	@ID bigint OUTPUT,
	@provider varchar(MAX),
	@accuracy float,
	@altitude float,
	@bearing float,	
	@speed float,
	@time bigint,
	@extras text,
	@logEntryID bigint,
	@latitude float,
	@longitude float
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE PositionLocationChanged SET provider = @provider, accuracy = @accuracy, altitude = @altitude, bearing = @bearing,
		speed = @speed, [time] = @time, extras = @extras, logEntryID = @logEntryID, latitude = @latitude,
		longitude = @longitude, position = Geography::Point(@latitude, @longitude, 4326)
	WHERE ID = @ID
END
GO

IF OBJECT_ID('dbo.UpdatePositionLocationChanged') IS NOT NULL
DROP PROCEDURE [dbo].UpdatePositionLocationChanged
GO

CREATE PROCEDURE [dbo].[UpdatePositionLocationChanged]
	@ID bigint,
	@provider varchar(MAX),
	@accuracy float,
	@altitude float,
	@bearing float,	
	@speed float,
	@time bigint,
	@extras text,
	@logEntryID bigint,
	@latitude float,
	@longitude float
AS
BEGIN
	SET NOCOUNT ON;
	INSERT PositionLocationChanged (provider, accuracy, altitude, bearing, speed, [time], extras, logEntryID, latitude, longitude, position)
	VALUES (@provider, @accuracy, @altitude, @bearing, @speed, @time, @extras, @logEntryID, @latitude, @longitude, Geography::Point(@latitude, @longitude, 4326))
	SET @ID = CAST(SCOPE_IDENTITY() AS bigint)
END
GO