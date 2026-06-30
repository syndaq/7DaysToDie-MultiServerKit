namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// Gametime
    /// </summary>
    public class GameTime
    {
        /// <summary>
        /// 
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// When
        /// </summary>
        public int Hours { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Minutes { get; set; }

        public GameTime()
        { 
        }

        public GameTime(int days, int hours, int minutes)
        {
            Days = days;
            Hours = hours;
            Minutes = minutes;
        }

        //public GameTime(ulong worldTime)
        //{
        //    Days = GameUtils.WorldTimeToDays(worldTime);
        //    Hours = GameUtils.WorldTimeToHours(worldTime);
        //    Minutes = GameUtils.WorldTimeToMinutes(worldTime);
        //}
    }
}