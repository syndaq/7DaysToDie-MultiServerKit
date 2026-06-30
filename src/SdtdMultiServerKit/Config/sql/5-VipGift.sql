--VIP
CREATE TABLE IF NOT EXISTS T_VipGift_v1(
	Id TEXT NOT NULL PRIMARY KEY,					--Player Id
	CreatedAt TEXT NOT NULL,						--Created date
	Name TEXT NOT NULL,								--Name
	ClaimState INTEGER NOT NULL,					--State
	TotalClaimCount INTEGER NOT NULL,				--
	LastClaimAt TEXT,								--Lastdate
	Description TEXT								--
);

--Enabledoutside
PRAGMA FOREIGN_KEYS = ON;

CREATE TABLE IF NOT EXISTS T_VipGiftItem_v1(
	VipGiftId TEXT NOT NULL,						--VIPId
	ItemId INTEGER NOT NULL,						--ItemId
	PRIMARY KEY (VipGiftId, ItemId),
	FOREIGN KEY (VipGiftId) REFERENCES T_VipGift_v1(Id) ON DELETE CASCADE,
	FOREIGN KEY (ItemId) REFERENCES T_ItemList(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS T_VipGiftCommand_v1(
	VipGiftId TEXT NOT NULL,						--VIPId
	CommandId INTEGER NOT NULL,						--CommandId
	PRIMARY KEY (VipGiftId, CommandId),
	FOREIGN KEY (VipGiftId) REFERENCES T_VipGift_v1(Id) ON DELETE CASCADE,
	FOREIGN KEY (CommandId) REFERENCES T_CommandList(Id) ON DELETE CASCADE
);