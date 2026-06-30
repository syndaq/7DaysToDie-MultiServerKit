using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace SdtdMultiServerKit.Data.Entities
{
    [Table("T_LevelGiftItem_v1")]
    public class T_LevelGiftItem
    {
        public string LevelGiftId { get; set; } = string.Empty;

        public int ItemId { get; set; }
    }
}
