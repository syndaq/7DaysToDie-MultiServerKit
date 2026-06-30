namespace SdtdMultiServerKit.FunctionSettings
{
    public class LotterySettings : SettingsBase
    {
        public string QueryListCmd { get; set; } = "lottery";

        public string DrawCmdPrefix { get; set; } = "draw";

        public int DrawCost { get; set; } = 10;

        public int DrawInterval { get; set; } = 60;

        public string PoolItemTip { get; set; } = string.Empty;

        public string DrawSuccessTip { get; set; } = string.Empty;

        public string PointsNotEnoughTip { get; set; } = string.Empty;

        public string CoolingTip { get; set; } = string.Empty;

        public string NoPoolTip { get; set; } = string.Empty;
    }
}
