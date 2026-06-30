--
CREATE TABLE IF NOT EXISTS T_ChatRecord(
	Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,	--Id
	CreatedAt TEXT NOT NULL,						--Created date
	PlayerId TEXT,									--Playercross-platformId
	EntityId INTEGER NOT NULL,						--EntityId
	SenderName TEXT NOT NULL,						--Name
	ChatType INTEGER NOT NULL,						--Type
	Message TEXT									--Message
);
--Create index
CREATE INDEX IF NOT EXISTS Index_ChatRecord_0 ON T_ChatRecord(PlayerId);
CREATE INDEX IF NOT EXISTS Index_ChatRecord_1 ON T_ChatRecord(EntityId);
CREATE INDEX IF NOT EXISTS Index_ChatRecord_2 ON T_ChatRecord(SenderName);

