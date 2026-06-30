namespace SdtdMultiServerKit.Models
{
    public class LevelGift
    {
        public string Id { get; set; } = string.Empty;

        public string GiftType { get; set; } = "Level";

        public string? DisplayName { get; set; }

        public required string Name { get; set; }

        public int RequiredLevel { get; set; }

        public bool ClaimState { get; set; }

        public int TotalClaimCount { get; set; }

        public string? Description { get; set; }
    }
}
