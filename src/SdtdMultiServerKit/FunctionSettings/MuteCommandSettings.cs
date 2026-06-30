namespace SdtdMultiServerKit.FunctionSettings
{
    public class MuteCommandSettings : SettingsBase
    {
        public string[] MutedCommands { get; set; } = Array.Empty<string>();

        public string MutedTip { get; set; } = string.Empty;
    }
}
