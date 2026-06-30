using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;
using SdtdMultiServerKit.Models;

namespace SdtdMultiServerKit.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/PvpArea")]
    public class PvpAreaController : ApiController
    {
        private readonly IPvpAreaRepository _pvpAreaRepository;

        public PvpAreaController(IPvpAreaRepository pvpAreaRepository)
        {
            _pvpAreaRepository = pvpAreaRepository;
        }

        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(T_PvpArea))]
        public async Task<IHttpActionResult> Get(string id)
        {
            var entity = await _pvpAreaRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<T_PvpArea>> Get()
        {
            return await _pvpAreaRepository.GetAllAsync();
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] PvpArea model)
        {
            var coords = NormalizeCoords(model);
            var entity = new T_PvpArea
            {
                Id = string.IsNullOrWhiteSpace(model.Id) ? Guid.NewGuid().ToString("N") : model.Id,
                CreatedAt = DateTime.Now,
                AreaNote = model.AreaNote,
                X1 = coords.x1,
                Z1 = coords.z1,
                X2 = coords.x2,
                Z2 = coords.z2,
                AreaNoticeBuff = model.AreaNoticeBuff,
                KillMode = model.KillMode,
                DropOnDeath = model.DropOnDeath,
                OnlineLandClaimBonus = model.OnlineLandClaimBonus,
                OfflineLandClaimBonus = model.OfflineLandClaimBonus,
                InvulnerableClaim = model.InvulnerableClaim,
                SortOrder = model.SortOrder,
            };
            await _pvpAreaRepository.InsertAsync(entity);
            await ReloadAreasAsync();
            return Ok(entity);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(string id, [FromBody] PvpArea model)
        {
            var entity = await _pvpAreaRepository.GetByIdAsync(id);
            if (entity == null)
            {
                entity = new T_PvpArea
                {
                    Id = id,
                    CreatedAt = DateTime.Now,
                };
                var coords = NormalizeCoords(model);
                entity.AreaNote = model.AreaNote;
                entity.X1 = coords.x1;
                entity.Z1 = coords.z1;
                entity.X2 = coords.x2;
                entity.Z2 = coords.z2;
                entity.AreaNoticeBuff = model.AreaNoticeBuff;
                entity.KillMode = model.KillMode;
                entity.DropOnDeath = model.DropOnDeath;
                entity.OnlineLandClaimBonus = model.OnlineLandClaimBonus;
                entity.OfflineLandClaimBonus = model.OfflineLandClaimBonus;
                entity.InvulnerableClaim = model.InvulnerableClaim;
                entity.SortOrder = model.SortOrder;
                await _pvpAreaRepository.InsertAsync(entity);
                await ReloadAreasAsync();
                return Ok(entity);
            }

            var normalized = NormalizeCoords(model);
            entity.AreaNote = model.AreaNote;
            entity.X1 = normalized.x1;
            entity.Z1 = normalized.z1;
            entity.X2 = normalized.x2;
            entity.Z2 = normalized.z2;
            entity.AreaNoticeBuff = model.AreaNoticeBuff;
            entity.KillMode = model.KillMode;
            entity.DropOnDeath = model.DropOnDeath;
            entity.OnlineLandClaimBonus = model.OnlineLandClaimBonus;
            entity.OfflineLandClaimBonus = model.OfflineLandClaimBonus;
            entity.InvulnerableClaim = model.InvulnerableClaim;
            entity.SortOrder = model.SortOrder;

            await _pvpAreaRepository.UpdateAsync(entity);
            await ReloadAreasAsync();
            return Ok(entity);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(string id)
        {
            int count = await _pvpAreaRepository.DeleteByIdAsync(id);
            if (count == 0)
            {
                return NotFound();
            }

            await ReloadAreasAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> DeleteAll()
        {
            int count = await _pvpAreaRepository.DeleteAllAsync();
            await ReloadAreasAsync();
            return Ok(count);
        }

        private static (int x1, int z1, int x2, int z2) NormalizeCoords(PvpArea model)
        {
            return (
                Math.Min(model.X1, model.X2),
                Math.Min(model.Z1, model.Z2),
                Math.Max(model.X1, model.X2),
                Math.Max(model.Z1, model.Z2));
        }

        private async Task ReloadAreasAsync()
        {
            var settings = ConfigManager.Get<PvpAreaSettings>() ?? new PvpAreaSettings();
            var areas = await _pvpAreaRepository.GetAllAsync();
            PvpAreaManager.Reload(settings, areas);
        }
    }
}
