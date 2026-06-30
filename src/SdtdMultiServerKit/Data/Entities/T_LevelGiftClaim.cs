using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace SdtdMultiServerKit.Data.Entities
{
    [Table("T_LevelGiftClaim_v1")]
    public class T_LevelGiftClaim
    {
        public string PlayerId { get; set; } = string.Empty;

        public string LevelGiftId { get; set; } = string.Empty;

        public DateTime ClaimedAt { get; set; }
    }
}
