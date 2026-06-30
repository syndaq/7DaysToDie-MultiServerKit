--Teleport record
CREATE TABLE IF NOT EXISTS T_TeleRecord(
	Id INTEGER PRIMARY KEY AUTOINCREMENT,	--UniqueId
	CreatedAt TEXT NOT NULL,				--Created date
	PlayerId TEXT NOT NULL,					--Playercross-platformId
	PlayerName TEXT NOT NULL,				--Player name
	TargetType TEXT NOT NULL,				--Targettype
	TargetId TEXT,							--TargetId
	TargetName TEXT NOT NULL,				--Target name
	OriginPosition TEXT NOT NULL,			--Coordinates, 
	TargetPosition TEXT	NOT NULL			--Coordinates, 
);
--Create index
CREATE INDEX IF NOT EXISTS Index_TeleRecord_0 ON T_TeleRecord(PlayerId);
CREATE INDEX IF NOT EXISTS Index_TeleRecord_1 ON T_TeleRecord(PlayerName);
CREATE INDEX IF NOT EXISTS Index_TeleRecord_2 ON T_TeleRecord(TargetType);

--
CREATE TABLE IF NOT EXISTS T_CityLocation_v1(
	Id INTEGER PRIMARY KEY,					--UniqueId
	CreatedAt TEXT NOT NULL,				--Created date
	CityName TEXT NOT NULL,					--Name
	PointsRequired INTEGER NOT NULL,		--Points required for teleport
	Position TEXT NOT NULL,					--3D coordinates
	ViewDirection TEXT						--View direction
);

--
CREATE TABLE IF NOT EXISTS T_HomeLocation(
	Id INTEGER PRIMARY KEY AUTOINCREMENT,	--UniqueId
	CreatedAt TEXT NOT NULL,				--Created date
	PlayerId TEXT NOT NULL,					--Playercross-platformId
	PlayerName TEXT NOT NULL,				--Player name	
	HomeName TEXT NOT NULL,					--HomeName
	Position TEXT NOT NULL					--3D coordinates
);
--Create index
CREATE UNIQUE INDEX IF NOT EXISTS Index_HomeLocation_0 ON T_HomeLocation(PlayerId, HomeName);

