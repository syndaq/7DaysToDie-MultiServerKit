using System.Collections.Generic;

namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// Statistics
    /// </summary>
    public class Stats
    {
        /// <summary>
        /// Server uptime
        /// </summary>
        public float Uptime { get; set; }

        /// <summary>
        /// Gametime
        /// </summary>
        public GameTime GameTime { get; set; } = null!;

        /// <summary>
        /// Animal count
        /// </summary>
        public int Animals { get; set; }

        /// <summary>
        /// Max Spawned Animals
        /// </summary>
        public int MaxAnimals { get; set; }

        /// <summary>
        /// Zombie count
        /// </summary>
        public int Zombies { get; set; }

        /// <summary>
        /// Max Spawned Zombies
        /// </summary>
        public int MaxZombies { get; set; }

        /// <summary>
        /// Entity
        /// </summary>
        public int Entities { get; set; }

        /// <summary>
        /// Online player
        /// </summary>
        public int OnlinePlayers { get; set; }

        /// <summary>
        /// MaximumOnline player
        /// </summary>
        public int MaxOnlinePlayers { get; set; }

        /// <summary>
        /// Player count
        /// </summary>
        public int OfflinePlayers { get; set; }

        /// <summary>
        /// Whetherasblood moon
        /// </summary>
        public bool IsBloodMoon { get; set; }

        /// <summary>
        /// Frames Per Second
        /// </summary>
        [JsonProperty("fps")]
        public float FPS { get; set; }

        /// <summary>
        /// Heapmemoryuse,  (MB) AsUnit, Heap memory used by the current game
        /// </summary>
        public float Heap { get; set; }

        /// <summary>
        /// Maximum heap memory limit， (MB) AsUnit，Maximum available heap memory capacity
        /// </summary>
        public float MaxHeap { get; set; }

        /// <summary>
        /// Number of chunks in the game
        /// </summary>
        public int Chunks { get; set; }

        /// <summary>
        /// Number of active objects in the current game
        /// </summary>
        [JsonProperty("cgo")]
        public int CGO { get; set; }

        /// <summary>
        /// Item
        /// </summary>
        public int Items { get; set; }

        /// <summary>
        /// CO
        /// </summary>
        public int ChunkObservedEntities { get; set; }

        /// <summary>
        /// Memory, Physical memory used by the current game
        /// </summary>
        public float ResidentSetSize { get; set; }

        /// <summary>
        /// Server Name
        /// </summary>
        public required string ServerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public required string Region { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public required string Language { get; set; }

        /// <summary>
        /// Server Version
        /// </summary>
        public required string ServerVersion { get; set; }

        /// <summary>
        /// Server IP
        /// </summary>
        public required string ServerIp { get; set; }

        /// <summary>
        /// ServerPort
        /// </summary>
        public int ServerPort { get; set; }

        /// <summary>
        /// Game Mode
        /// </summary>
        public required string GameMode { get; set; }

        /// <summary>
        /// Game World
        /// </summary>
        public required string GameWorld { get; set; }

        /// <summary>
        /// Game Name
        /// </summary>
        public required string GameName { get; set; }

        /// <summary>
        /// Game Difficulty
        /// </summary>
        public int GameDifficulty { get; set; }
    }
}