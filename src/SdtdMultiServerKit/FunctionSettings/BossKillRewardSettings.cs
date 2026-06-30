namespace SdtdMultiServerKit.FunctionSettings
{
    public class BossKillRewardSettings : SettingsBase
    {
        public string KillTip { get; set; } = string.Empty;

        public Dictionary<string, int> EnemyRewardMap { get; set; } = new();

        public int FallbackReward { get; set; } = 1;
    }
}
