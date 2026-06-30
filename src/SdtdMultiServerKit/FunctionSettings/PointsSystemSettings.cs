namespace SdtdMultiServerKit.FunctionSettings
{
    public class PointsSystemSettings : SettingsBase
    {
        /// <summary>
        /// Sign-incommand
        /// </summary>
        public string SignInCmd { get; set; }

        /// <summary>
        /// Sign-inintervalseconds
        /// </summary>
        public int SignInInterval { get; set; }

        /// <summary>
        /// Sign-in reward points
        /// </summary>
        public int SignInRewardPoints { get; set; }

        /// <summary>
        /// Sign-in
        /// </summary>
        public string SignInSuccessTip { get; set; }

        /// <summary>
        /// Sign-in
        /// </summary>
        public string SignInFailureTip { get; set; }

        /// <summary>
        /// Pointscommand
        /// </summary>
        public string QueryPointsCmd { get; set; }

        /// <summary>
        /// Points
        /// </summary>
        public string QueryPointsTip { get; set; }

        /// <summary>
        /// Whether in-game currency to points exchange is enabled
        /// </summary>
        public bool IsCurrencyExchangeEnabled { get; set; }

        /// <summary>
        /// In-game currency to points exchange ratio
        /// </summary>
        public double CurrencyToPointsExchangeRate { get; set; }

        /// <summary>
        /// Exchangecommand
        /// </summary>
        public string CurrencyExchangeCmd { get; set; }

        /// <summary>
        /// Exchange
        /// </summary>
        public string ExchangeSuccessTip { get; set; }

        /// <summary>
        /// Exchange
        /// </summary>
        public string ExchangeFailureTip { get; set; }
    }
}
