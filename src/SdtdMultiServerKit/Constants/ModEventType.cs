namespace SdtdMultiServerKit.Constants
{
    /// <summary>
    /// ModEventType
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ModEventType
    {
        /// <summary>
        /// Welcome
        /// </summary>
        Welcome,

        /// <summary>
        /// Command execution reply
        /// </summary>
        CommandExecutionReply,

        /// <summary>
        /// Callback
        /// </summary>
        LogCallback,

        /// <summary>
        /// Game
        /// </summary>
        GameAwake,

        /// <summary>
        /// Gamestartup
        /// </summary>
        GameStartDone,

        /// <summary>
        /// Game
        /// </summary>
        GameUpdate,

        /// <summary>
        /// Game
        /// </summary>
        GameShutdown,

        /// <summary>
        /// Chunk
        /// </summary>
        CalcChunkColorsDone,

        /// <summary>
        /// Chat message
        /// </summary>
        ChatMessage,

        /// <summary>
        /// Entityby
        /// </summary>
        EntityKilled,

        /// <summary>
        /// Entity
        /// </summary>
        EntitySpawned,

        /// <summary>
        /// Player
        /// </summary>
        PlayerDisconnected,

        /// <summary>
        /// Player
        /// </summary>
        PlayerLogin,

        /// <summary>
        /// Playerinin
        /// </summary>
        PlayerSpawnedInWorld,

        /// <summary>
        /// Player
        /// </summary>
        PlayerSpawning,

        /// <summary>
        /// Player count
        /// </summary>
        SavePlayerData,

        /// <summary>
        /// 
        /// </summary>
        SkyChanged,
    }
}
