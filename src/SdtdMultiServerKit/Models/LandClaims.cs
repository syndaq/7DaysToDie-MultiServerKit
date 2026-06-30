namespace SdtdMultiServerKit.Models
{
    /// <summary>
    /// Land claim
    /// </summary>
    public class LandClaims
    {
        /// <summary>
        /// Land claimowned
        /// </summary>
        public IEnumerable<ClaimOwner> ClaimOwners { get; set; }

        /// <summary>
        /// Land claimscope
        /// </summary>
        public int ClaimSize { get; set; }
    }
}