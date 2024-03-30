SET NOCOUNT ON
SET XACT_ABORT ON

PRINT 'Installing Boba.Settings SQL objects...';

-- Create the database schema if it doesn't exists
IF NOT EXISTS (SELECT [schema_id] FROM [sys].[schemas] WHERE [name] = '$(BobaSchema)')
BEGIN
    EXEC (N'CREATE SCHEMA [$(BobaSchema)]');
    PRINT 'Created database schema [$(BobaSchema)]';
END
ELSE
    PRINT 'Database schema [$(BobaSchema)] already exists';


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