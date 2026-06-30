namespace SdtdMultiServerKit.Models
{
    public class LotteryPool
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public int DrawCost { get; set; }

        public int Weight { get; set; } = 1;

        public bool IsEnabled { get; set; } = true;

        public string? Description { get; set; }
    }
}
