using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace SdtdMultiServerKit.Data.Entities
{
    [Table("T_LotteryPool_v1")]
    public class T_LotteryPool
    {
        [PrimaryKey]
        public int Id { get; set; }

        [IgnoreUpdate]
        public DateTime CreatedAt { get; set; }

        public required string Name { get; set; }

        public int DrawCost { get; set; }

        public int Weight { get; set; } = 1;

        public bool IsEnabled { get; set; } = true;

        public string? Description { get; set; }
    }
}
