namespace SdtdMultiServerKit.Models
{
    public class PvpArea
    {
        public string Id { get; set; } = string.Empty;

        public string AreaNote { get; set; } = string.Empty;

        public int X1 { get; set; }

        public int Z1 { get; set; }

        public int X2 { get; set; }

        public int Z2 { get; set; }

        public string AreaNoticeBuff { get; set; } = string.Empty;

        public string KillMode { get; set; } = "strangers";

        public string DropOnDeath { get; set; } = "none";

        public int OnlineLandClaimBonus { get; set; }

        public int OfflineLandClaimBonus { get; set; }

        public bool InvulnerableClaim { get; set; }

        public int SortOrder { get; set; }
    }
}
