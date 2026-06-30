namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Inventory
    {
        /// <summary>
        /// Inventory
        /// </summary>
        public IEnumerable<InvItem> Bag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<InvItem> Belt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<InvItem> Equipment { get; set; }
    }
}
