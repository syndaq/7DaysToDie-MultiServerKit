--Commandlist
CREATE TABLE IF NOT EXISTS T_CommandList(
	Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,	--Id
	CreatedAt TEXT NOT NULL,						--Created date
	Command TEXT NOT NULL,							--Command
	InMainThread INTEGER NOT NULL,					--Inexecution
	Description TEXT								--
);
