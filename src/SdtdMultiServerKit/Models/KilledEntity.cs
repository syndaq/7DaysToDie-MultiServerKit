namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// Bykillinfo
    /// </summary>
    public class KilledEntity
    {
        /// <summary>
        /// Killed entity information
        /// </summary>
        public EntityInfo DeadEntity { get; set; } = null!;

        /// <summary>
        /// KillentityId
        /// </summary>
        public int KillerEntityId { get; set; }
    }
}