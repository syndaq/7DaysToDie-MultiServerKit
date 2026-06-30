--Points info
CREATE TABLE IF NOT EXISTS T_PointsInfo(
	Id TEXT NOT NULL PRIMARY KEY,					--Playercross-platformId
	CreatedAt TEXT NOT NULL,						--Created date
	PlayerName TEXT NOT NULL,						--Player name
	Points INTEGER NOT NULL,						--Points balance
	LastSignInDays INTEGER NOT NULL					--Last sign-in day count
);

--Create index
CREATE INDEX IF NOT EXISTS Index_PointsInfo_0 ON T_PointsInfo(CreatedAt);
CREATE INDEX IF NOT EXISTS Index_PointsInfo_2 ON T_PointsInfo(PlayerName);
CREATE INDEX IF NOT EXISTS Index_PointsInfo_3 ON T_PointsInfo(Points);

--Points info v1
CREATE TABLE IF NOT EXISTS T_PointsInfo_v1(
	Id TEXT NOT NULL PRIMARY KEY,					--Playercross-platformId
	CreatedAt TEXT NOT NULL,						--Created date
	PlayerName TEXT NOT NULL,						--Player name
	Points INTEGER NOT NULL,						--Points balance
	LastSignInAt TEXT								--Last sign-in date
);

--Create index
CREATE INDEX IF NOT EXISTS Index_PointsInfo_2 ON T_PointsInfo_v1(PlayerName);
CREATE INDEX IF NOT EXISTS Index_PointsInfo_3 ON T_PointsInfo_v1(Points);

--Migrate data to new table
INSERT INTO T_PointsInfo_v1 (Id, CreatedAt, PlayerName, Points)
SELECT 
    Id, 
    CreatedAt, 
    PlayerName, 
    Points
FROM 
    T_PointsInfo
   WHERE NOT EXISTS(SELECT 1 FROM T_PointsInfo_v1);