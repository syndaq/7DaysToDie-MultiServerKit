namespace SdtdMultiServerKit.Models
{
    public class CityLocation
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// City name
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Points required for teleport
        /// </summary>
        public int PointsRequired { get; set; }

        /// <summary>
        /// 3D coordinates
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// View direction
        /// </summary>
        public string? ViewDirection { get; set; }
    }
}
