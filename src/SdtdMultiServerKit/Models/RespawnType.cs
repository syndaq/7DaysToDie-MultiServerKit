namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// Type
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RespawnType
    {
        /// <summary>
        /// Game
        /// </summary>
        NewGame = 0,

        /// <summary>
        /// Loadgame
        /// </summary>
        LoadedGame = 1,

        /// <summary>
        /// 
        /// </summary>
        Died = 2,

        /// <summary>
        /// Teleport
        /// </summary>
        Teleport = 3,

        /// <summary>
        /// New player joined multiplayer
        /// </summary>
        EnterMultiplayer = 4,

        /// <summary>
        /// Returning player joined multiplayer
        /// </summary>
        JoinMultiplayer = 5,

        /// <summary>
        /// 
        /// </summary>
        Unknown = 6
    }
}