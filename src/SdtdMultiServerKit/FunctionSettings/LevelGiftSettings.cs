namespace SdtdMultiServerKit.FunctionSettings
{
    public class LevelGiftSettings : SettingsBase
    {
        public required string ClaimCmd { get; set; }

        public required string HasClaimedTip { get; set; }

        public required string LevelNotEnoughTip { get; set; }

        public required string NoGiftTip { get; set; }

        public required string ClaimSuccessTip { get; set; }
    }
}
