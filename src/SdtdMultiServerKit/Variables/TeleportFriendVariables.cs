namespace SdtdMultiServerKit.Variables
{
    public class TeleportFriendVariables : VariablesBase
    {
        /// <summary>
        /// Teleport interval
        /// </summary>
        public int TeleInterval { get; set; }

        /// <summary>
        /// Points required
        /// </summary>
        public int PointsRequired { get; set; }

        /// <summary>
        /// Target name
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// Cooldown time
        /// </summary>
        public int CooldownSeconds { get; set; }
    }
}