using SdtdMultiServerKit.Variables;

namespace SdtdMultiServerKit.Variables
{
    public class LotteryVariables : VariablesBase
    {
        public int PoolId { get; set; }

        public string? PoolName { get; set; }

        public int DrawCost { get; set; }

        public int Weight { get; set; }
    }
}
