namespace SdtdMultiServerKit.Utilities
{
    /// <summary>
    /// Class
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Get item count in player inventory
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="itemName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int GetPlayerInventoryStackCount(string playerId, string itemName)
        {
            var userId = PlatformUserIdentifierAbs.FromCombinedString(playerId);
            if (userId == null)
            {
                throw new Exception("Invalid player id.");
            }

            var clientInfo = ConnectionManager.Instance.Clients.ForUserId(userId);
            if (clientInfo == null)
            {
                throw new Exception("Player not found.");
            }

            return clientInfo.latestPlayerData.GetInventoryStackCount(itemName, Language.English);
        }

        /// <summary>
        /// Playeritem
        /// </summary>
        /// <param name="playerIdOrName"></param>
        /// <param name="itemName"></param>
        /// <param name="count"></param>
        /// <param name="quality"></param>
        /// <param name="durability"></param>
        public static void GiveItem(string playerIdOrName, string itemName, int count, int quality = 0, int durability = 0)
        {
            ExecuteConsoleCommand($"ty-gi {FormatCommandArgs(playerIdOrName)} {FormatCommandArgs(itemName)} {count} {quality} {durability}");
        }

        /// <summary>
        /// Check whether zombies are near the player
        /// </summary>
        /// <param name="entityPlayer"></param>
        /// <returns></returns>
        public static bool ZombieCheck(EntityPlayer entityPlayer)
        {
            var entities = GameManager.Instance.World.Entities.list;
            for (int i = 0; i < entities.Count; i++)
            {
                Entity entity = entities[i];
                if (entity is EntityZombie zombie && zombie.IsSpawned() && zombie.IsAlive())
                {
                    if ((entityPlayer.serverPos.ToVector3() / 32F - entity.position).sqrMagnitude <= 10 * 10)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Determine whether two players are friends
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="anotherEntityId"></param>
        /// <returns></returns>
        public static bool IsFriend(int entityId, int anotherEntityId)
        {
            var players = GameManager.Instance.World.Players.dict;
            if (players.TryGetValue(entityId, out var entityPlayer) == false)
            {
                return false;
            }

            if (players.TryGetValue(anotherEntityId, out var anotherEntityPlayer) == false)
            {
                return false;
            }

            return entityPlayer.IsFriendsWith(anotherEntityPlayer);
        }

        /// <summary>
        /// Getplayerlocation
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public static Position GetPlayerPosition(int entityId)
        {
            if (GameManager.Instance.World.Players.dict.TryGetValue(entityId, out EntityPlayer player))
            {
                return player.position.ToPosition();
            }

            var clientInfo = ConnectionManager.Instance.Clients.ForEntityId(entityId);
            if (clientInfo != null)
            {
                if (GameManager.Instance.GetPersistentPlayerList().Players.TryGetValue(clientInfo.InternalId, out PersistentPlayerData persistentPlayerData))
                {
                    return persistentPlayerData.Position.ToPosition();
                }
            }

            throw new Exception("Player not found.");
        }

        /// <summary>
        /// Teleportplayer
        /// </summary>
        /// <param name="originPlayerIdOrName"></param>
        /// <param name="targetPlayerIdOrNameOrPosition"></param>
        /// <param name="viewDirection"></param>
        /// <returns></returns>
        public static IEnumerable<string> TeleportPlayer(string originPlayerIdOrName, string targetPlayerIdOrNameOrPosition, string? viewDirection = null)
        {
            string target = targetPlayerIdOrNameOrPosition;
            if (target.Split(' ').Length != 3)
            {
                target = FormatCommandArgs(target);
            }

            if (viewDirection == null)
            {
                return ExecuteConsoleCommand($"tele {FormatCommandArgs(originPlayerIdOrName)} {target}");
            }
            else
            {
                return ExecuteConsoleCommand($"tele {FormatCommandArgs(originPlayerIdOrName)} {target} {viewDirection}");
            }
        }

        /// <summary>
        /// Globalmessage
        /// </summary>
        /// <param name="globalMessage"></param>
        /// <returns></returns>
        public static IEnumerable<string> SendGlobalMessage(GlobalMessage globalMessage)
        {
            string cmd = string.Format("ty-say {0} {1}",
                FormatCommandArgs(globalMessage.Message),
                FormatCommandArgs(globalMessage.SenderName));
            return ExecuteConsoleCommand(cmd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="privateMessage"></param>
        /// <returns></returns>
        public static IEnumerable<string> SendPrivateMessage(PrivateMessage privateMessage)
        {
            string cmd = string.Format("ty-pm {0} {1} {2}",
                FormatCommandArgs(privateMessage.TargetPlayerIdOrName),
                FormatCommandArgs(privateMessage.Message),
                FormatCommandArgs(privateMessage.SenderName));

            return ExecuteConsoleCommand(cmd);
        }

        /// <summary>
        /// Executioncommand
        /// </summary>
        /// <param name="command"></param>
        /// <param name="inMainThread"></param>
        /// <returns></returns>
        public static IEnumerable<string> ExecuteConsoleCommand(string command, bool inMainThread = false)
        {
            if (inMainThread && ThreadManager.IsMainThread() == false)
            {
                IEnumerable<string> executeResult = Enumerable.Empty<string>();
                ModApi.MainThreadSyncContext.Send((state) =>
                {
                    executeResult = SdtdConsole.Instance.ExecuteSync((string)state, ModApi.CmdExecuteDelegate);
                }, command);

                return executeResult;
            }
            else
            {
                return SdtdConsole.Instance.ExecuteSync(command, ModApi.CmdExecuteDelegate);
            }
        }

        /// <summary>
        /// Commandparameter
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string FormatCommandArgs(string? args)
        {
            if (args == null || args.Length == 0)
            {
                return string.Empty;
            }

            if (args[0] == '\"' && args[args.Length - 1] == '\"')
            {
                return args;
            }

            if (args.Contains('\"'))
            {
                throw new Exception("Parameters should not contain the character double quotes.");
            }

            if (args.Contains(' '))
            {
                return string.Concat("\"", args, "\"");
            }

            return args;
        }

        /// <summary>
        /// Getlocalizationstring
        /// </summary>
        /// <param name="key"></param>
        /// <param name="language"></param>
        /// <param name="caseInsensitive"></param>
        /// <returns></returns>
        public static string GetLocalization(string key, Language language, bool caseInsensitive = false)
        {
            try
            {
                var dict = caseInsensitive
                    ? Compat.GameCompatBridge.LocalizationDictionaryCaseInsensitive
                    : Compat.GameCompatBridge.LocalizationDictionary;
                if (dict.TryGetValue(key, out string[] values))
                {
                    return values[(int)language] ?? string.Empty;
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get BlockValue
        /// </summary>
        /// <param name="blockIdOrName"></param>
        /// <param name="blockValue"></param>
        /// <returns></returns>
        public static bool TryGetBlockValue(string blockIdOrName, out BlockValue blockValue)
        {
            if (int.TryParse(blockIdOrName, out var blockId))
            {
                foreach (Block block in Block.list)
                {
                    if (block.blockID == blockId)
                    {
                        blockValue = Block.GetBlockValue(block.GetBlockName(), false);
                        return true;
                    }
                }
            }
            else
            {
                foreach (Block block in Block.list)
                {
                    string blockName = block.GetBlockName();
                    if (string.Equals(blockName, blockIdOrName, StringComparison.OrdinalIgnoreCase))
                    {
                        blockValue = Block.GetBlockValue(block.GetBlockName(), false);
                        return true;
                    }
                }
            }

            blockValue = BlockValue.Air;
            return false;
        }
    }
}