namespace SdtdMultiServerKit.WebApi.Controllers
{
    /// <summary>
    /// Land Claims
    /// </summary>
    [RoutePrefix("api/LandClaims")]
    public class LandClaimsController : ApiController
    {
        /// <summary>
        /// Get land claims for the specified player
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{playerId}")]
        [ResponseType(typeof(ClaimOwner))]
        public IHttpActionResult GetLandClaim(string playerId)
        {
            var userId = PlatformUserIdentifierAbs.FromCombinedString(playerId);
            if (userId == null)
            {
                return NotFound();
            }

            var persistentPlayerData = GameManager.Instance.GetPersistentPlayerList().GetPlayerData(userId);
            if (persistentPlayerData == null)
            {
                return NotFound();
            }

            var claimOwner = new ClaimOwner()
            {
                EntityId = persistentPlayerData.EntityId,
                PlatformId = persistentPlayerData.NativeId.CombinedString,
                PlayerId = persistentPlayerData.PrimaryId.CombinedString,
                PlayerName = persistentPlayerData.PlayerName.DisplayName,
                ClaimActive = GameManager.Instance.World.IsLandProtectionValidForPlayer(persistentPlayerData),
                ClaimPositions = persistentPlayerData.LPBlocks.ToPositions(),
                LastLogin = persistentPlayerData.LastLogin
            };

            return Ok(claimOwner);
        }

        /// <summary>
        /// Get land claims for all players
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public LandClaims GetLandClaims()
        {
            var claimOwners = new List<ClaimOwner>();

            foreach (var item in GameManager.Instance.GetPersistentPlayerList().Players)
            {
                var persistentPlayerData = item.Value;
                if(persistentPlayerData != null && persistentPlayerData.LPBlocks != null)
                {
                    claimOwners.Add(new ClaimOwner()
                    {
                        EntityId = persistentPlayerData.EntityId,
                        PlatformId = persistentPlayerData.NativeId.CombinedString,
                        PlayerId = persistentPlayerData.PrimaryId.CombinedString,
                        PlayerName = persistentPlayerData.PlayerName.DisplayName,
                        ClaimActive = GameManager.Instance.World.IsLandProtectionValidForPlayer(persistentPlayerData),
                        ClaimPositions = persistentPlayerData.LPBlocks.ToPositions(),
                        LastLogin = persistentPlayerData.LastLogin
                    });
                }
            }

            return new LandClaims()
            {
                ClaimOwners = claimOwners,
                ClaimSize = GamePrefs.GetInt(EnumGamePrefs.LandClaimSize)
            };
        }
    }
}
