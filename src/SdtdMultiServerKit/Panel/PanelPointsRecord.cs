namespace SdtdMultiServerKit.Panel
{
    public class PanelPointsRecord
    {
        public string PlatformId { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public int Points { get; set; }
        public DateTime? LastSignInAt { get; set; }
    }
}
