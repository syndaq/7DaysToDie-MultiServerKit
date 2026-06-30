--Itemlist
CREATE TABLE IF NOT EXISTS T_ItemList(
	Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,	--Id
	CreatedAt TEXT NOT NULL,						--Created date
	ItemName TEXT NOT NULL,							--ItemName
	[Count] INTEGER NOT NULL,						--Count
	Quality INTEGER NOT NULL,						--
	Durability INTEGER NOT NULL,					--
	Description TEXT								--
);

