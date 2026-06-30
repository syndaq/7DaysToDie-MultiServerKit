namespace SdtdMultiServerKit.Models
{
    public class ChunkResetRequest
    {
        public int X1 { get; set; }

        public int Z1 { get; set; }

        public int X2 { get; set; }

        public int Z2 { get; set; }
    }

    public class ChunkResetResult
    {
        public int ResetCount { get; set; }

        public int SkippedCount { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
