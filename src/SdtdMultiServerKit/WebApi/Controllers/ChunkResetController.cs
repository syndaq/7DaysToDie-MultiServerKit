using SdtdMultiServerKit.Models;

namespace SdtdMultiServerKit.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/ChunkReset")]
    public class ChunkResetController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult ResetRegion([FromBody] ChunkResetRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request body is required.");
            }

            int minX = Math.Min(request.X1, request.X2);
            int maxX = Math.Max(request.X1, request.X2);
            int minZ = Math.Min(request.Z1, request.Z2);
            int maxZ = Math.Max(request.Z1, request.Z2);

            if (minX == maxX && minZ == maxZ)
            {
                return BadRequest("Region must cover at least one block.");
            }

            var world = GameManager.Instance.World;
            if (world.ChunkCache.ChunkProvider is not ChunkProviderGenerateWorld chunkProvider)
            {
                return BadRequest("The active chunk provider does not support chunk reset.");
            }

            int minChunkX = World.toChunkXZ(minX);
            int maxChunkX = World.toChunkXZ(maxX);
            int minChunkZ = World.toChunkXZ(minZ);
            int maxChunkZ = World.toChunkXZ(maxZ);

            int resetCount = 0;
            int skippedCount = 0;

            for (int chunkX = minChunkX; chunkX <= maxChunkX; chunkX++)
            {
                for (int chunkZ = minChunkZ; chunkZ <= maxChunkZ; chunkZ++)
                {
                    if (world.GetChunkSync(chunkX, chunkZ) == null)
                    {
                        skippedCount++;
                        continue;
                    }

                    long chunkKey = WorldChunkCache.MakeChunkKey(chunkX, chunkZ);
                    chunkProvider.RequestChunkReset(chunkKey);
                    resetCount++;
                }
            }

            CustomLogger.Info("Chunk reset requested for region ({0},{1})-({2},{3}): reset={4}, skipped={5}.", minX, minZ, maxX, maxZ, resetCount, skippedCount);

            return Ok(new ChunkResetResult
            {
                ResetCount = resetCount,
                SkippedCount = skippedCount,
                Message = $"Requested reset for {resetCount} chunk(s); skipped {skippedCount} unloaded chunk(s).",
            });
        }
    }
}
