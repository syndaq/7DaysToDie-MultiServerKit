using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace SdtdMultiServerKit.Data.Entities
{
    [Table("T_LevelGiftCommand_v1")]
    public class T_LevelGiftCommand
    {
        public required string LevelGiftId { get; set; }

        public int CommandId { get; set; }
    }
}
