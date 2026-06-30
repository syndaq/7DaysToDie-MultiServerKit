using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace SdtdMultiServerKit.Data.Entities
{
    [Table("T_LevelGift_v1")]
    public class T_LevelGift
    {
        [PrimaryKey]
        public string Id { get; set; } = string.Empty;

        [IgnoreUpdate]
        public DateTime CreatedAt { get; set; }

        public string GiftType { get; set; } = "Level";

        public string? DisplayName { get; set; }

        public required string Name { get; set; }

        public int RequiredLevel { get; set; }

        public bool ClaimState { get; set; }

        public int TotalClaimCount { get; set; }

        public DateTime? LastClaimAt { get; set; }

        public string? Description { get; set; }
    }
}
