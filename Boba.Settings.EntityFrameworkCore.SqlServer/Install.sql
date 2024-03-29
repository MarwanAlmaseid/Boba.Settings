SET NOCOUNT ON
SET XACT_ABORT ON

PRINT 'Installing Boba.Settings SQL objects...';

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '$(BobaSchema)' AND TABLE_NAME = 'Setting')
BEGIN
    EXEC('CREATE TABLE $(BobaSchema).Setting
	(
		[Id]		INT				PRIMARY KEY 	IDENTITY(1,1),
		[Name]		VARCHAR(512)	NOT NULL,
		[Value]		NVARCHAR(MAX)	NULL,
	)')

	EXEC('CREATE INDEX idx_setting_name ON $(BobaSchema).Setting ([Name])')
END