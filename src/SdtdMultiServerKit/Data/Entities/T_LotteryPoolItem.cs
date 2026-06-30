using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace SdtdMultiServerKit.Data.Entities
{
    public class T_LotteryPoolItem
    {
        [PrimaryKey]
        public int LotteryPoolId { get; set; }

        [PrimaryKey]
        public int ItemId { get; set; }
    }
}
