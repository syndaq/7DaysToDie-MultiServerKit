namespace SdtdMultiServerKit.FunctionSettings
{
    public class TeleportCitySettings : SettingsBase
    {
        /// <summary>
        /// Listcommand
        /// </summary>
        public string QueryListCmd { get; set; }

        /// <summary>
        /// Teleportcommandbefore
        /// </summary>
        public string TeleCmdPrefix { get; set; }

        /// <summary>
        /// Teleport interval, Unit: Seconds
        /// </summary>
        public int TeleInterval { get; set; }

        /// <summary>
        /// List
        /// </summary>
        public string LocationItemTip { get; set; }

        /// <summary>
        /// Teleport
        /// </summary>
        public string TeleSuccessTip { get; set; }

        /// <summary>
        /// Pointsnot
        /// </summary>
        public string PointsNotEnoughTip { get; set; }

        /// <summary>
        /// Incooldown
        /// </summary>
        public string CoolingTip { get; set; }

        /// <summary>
        /// Nocityinfo
        /// </summary>
        public string NoLocation { get; set; }
    }
}