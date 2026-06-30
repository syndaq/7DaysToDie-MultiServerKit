--Goods
CREATE TABLE IF NOT EXISTS T_Goods_v2(
	Id INTEGER NOT NULL PRIMARY KEY,				--Id
	CreatedAt TEXT NOT NULL,						--Created date
	Name TEXT NOT NULL,								--GoodsName
	Price INTEGER NOT NULL,							--
	Description TEXT								--
);

--Enabledoutside
PRAGMA FOREIGN_KEYS = ON;

CREATE TABLE IF NOT EXISTS T_GoodsItem(
	GoodsId INTEGER NOT NULL,						--GoodsId
	ItemId INTEGER NOT NULL,						--ItemId
	PRIMARY KEY (GoodsId, ItemId),
	FOREIGN KEY (GoodsId) REFERENCES T_Goods_v2(Id) ON DELETE CASCADE,
	FOREIGN KEY (ItemId) REFERENCES T_ItemList(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS T_GoodsCommand(
	GoodsId INTEGER NOT NULL,						--GoodsId
	CommandId INTEGER NOT NULL,						--CommandId
	PRIMARY KEY (GoodsId, CommandId),
	FOREIGN KEY (GoodsId) REFERENCES T_Goods_v2(Id) ON DELETE CASCADE,
	FOREIGN KEY (CommandId) REFERENCES T_CommandList(Id) ON DELETE CASCADE
);