namespace SdtdMultiServerKit.FunctionSettings
{
    public class PvpAreaSettings : SettingsBase
    {
        public string KillMode { get; set; } = "strangers";

        public string DropOnDeath { get; set; } = "none";

        public int OnlineLandClaimBonus { get; set; } = 4;

        public int OfflineLandClaimBonus { get; set; } = 8;

        public string DefaultNoticeBuff { get; set; } = "buffPvpVeNoticePve";
    }
}
