namespace SdtdMultiServerKit.FunctionSettings
{
    public class GameNoticeSettings : SettingsBase
    {
        /// <summary>
        /// Welcome
        /// </summary>
        public string WelcomeNotice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string[] RotatingNotices { get; set; }

        /// <summary>
        /// Interval
        /// </summary>
        public int RotatingInterval { get; set; }

        /// <summary>
        /// Blood moon1, Belowblood moonin {BloodMoonDays} After
        /// </summary>
        public string BloodMoonNotice1 { get; set; }

        /// <summary>
        /// Blood moon2, Next blood moon is today。 {BloodMoonStartTime} 
        /// </summary>
        public string BloodMoonNotice2 { get; set; }

        /// <summary>
        /// Blood moon3, Blood moon, To {BloodMoonEndTime}
        /// </summary>
        public string BloodMoonNotice3 { get; set; }
    }
}