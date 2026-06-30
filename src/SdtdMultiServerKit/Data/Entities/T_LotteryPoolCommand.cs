using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace SdtdMultiServerKit.Data.Entities
{
    public class T_LotteryPoolCommand
    {
        [PrimaryKey]
        public int LotteryPoolId { get; set; }

        [PrimaryKey]
        public int CommandId { get; set; }
    }
}
