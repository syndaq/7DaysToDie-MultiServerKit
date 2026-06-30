namespace SdtdMultiServerKit.Variables
{
    public class TeleportCityVariables : VariablesBase
    {
        /// <summary>
        /// CityId
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// City name
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Teleport interval
        /// </summary>
        public int TeleInterval { get; set; }

        /// <summary>
        /// Points required
        /// </summary>
        public int PointsRequired { get; set; }

        /// <summary>
        /// Cooldown time, Unit: Seconds
        /// </summary>
        public int CooldownSeconds { get; set; }
    }
}