using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;
using SdtdMultiServerKit.Models;
using System.ComponentModel.DataAnnotations;

namespace SdtdMultiServerKit.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/LevelGift")]
    public class LevelGiftController : ApiController
    {
        private readonly ILevelGiftRepository _levelGiftRepository;
        private readonly IItemListRepository _itemListRepository;
        private readonly ICommandListRepository _commandListRepository;

        public LevelGiftController(
            ILevelGiftRepository levelGiftRepository,
            IItemListRepository itemListRepository,
            ICommandListRepository commandListRepository)
        {
            _levelGiftRepository = levelGiftRepository;
            _itemListRepository = itemListRepository;
            _commandListRepository = commandListRepository;
        }

        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(T_LevelGift))]
        public async Task<IHttpActionResult> Get(string id)
        {
            var entity = await _levelGiftRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<T_LevelGift>> Get()
        {
            return await _levelGiftRepository.GetAllAsync();
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] LevelGift model)
        {
            var entity = new T_LevelGift
            {
                Id = model.Id,
                GiftType = model.GiftType,
                DisplayName = model.DisplayName,
                Name = model.Name,
                RequiredLevel = model.RequiredLevel,
                CreatedAt = DateTime.Now,
                ClaimState = model.ClaimState,
                TotalClaimCount = model.TotalClaimCount,
                Description = model.Description,
            };
            await _levelGiftRepository.InsertAsync(entity);
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(string id, [FromBody] LevelGift model)
        {
            var entity = await _levelGiftRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.GiftType = model.GiftType;
            entity.DisplayName = model.DisplayName;
            entity.Name = model.Name;
            entity.RequiredLevel = model.RequiredLevel;
            entity.ClaimState = model.ClaimState;
            entity.TotalClaimCount = model.TotalClaimCount;
            entity.Description = model.Description;

            await _levelGiftRepository.UpdateAsync(entity);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(string id)
        {
            int count = await _levelGiftRepository.DeleteByIdAsync(id);
            if (count == 0)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> Delete([FromUri] string[]? ids, [FromUri] bool deleteAll = false, [FromUri] bool resetAll = false)
        {
            int count = 0;

            if (deleteAll)
            {
                count = await _levelGiftRepository.DeleteAllAsync(true);
            }
            else if (resetAll)
            {
                count = await _levelGiftRepository.ResetClaimStateAsync();
            }
            else if (ids != null && ids.Length > 0)
            {
                count = await _levelGiftRepository.DeleteByIdsAsync(ids, true);
            }

            return Ok(count);
        }

        [HttpGet]
        [Route("{id}/Items")]
        public async Task<IEnumerable<T_ItemList>> GetItems(string id)
        {
            return await _itemListRepository.GetListByLevelGiftIdAsync(id);
        }

        [HttpPut]
        [Route("{id}/Items")]
        public async Task<IHttpActionResult> PutItems(string id, [FromBody, Required] int[] itemIds)
        {
            var entity = await _levelGiftRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var entities = itemIds.Select(itemId => new T_LevelGiftItem
            {
                LevelGiftId = id,
                ItemId = itemId,
            }).ToList();

            using var unitOfWork = ModApi.ServiceContainer.Resolve<IUnitOfWorkFactory>().Create();
            var levelGiftItemRepository = unitOfWork.GetRepository<ILevelGiftItemRepository>();
            await levelGiftItemRepository.DeleteByLevelGiftIdAsync(id);
            await levelGiftItemRepository.InsertAsync(entities);
            unitOfWork.Commit();

            return Ok();
        }

        [HttpGet]
        [Route("{id}/Commands")]
        public async Task<IEnumerable<T_CommandList>> GetCommands(string id)
        {
            return await _commandListRepository.GetListByLevelGiftIdAsync(id);
        }

        [HttpPut]
        [Route("{id}/Commands")]
        public async Task<IHttpActionResult> PutCommands(string id, [FromBody, Required] int[] itemIds)
        {
            var entity = await _levelGiftRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var entities = itemIds.Select(commandId => new T_LevelGiftCommand
            {
                LevelGiftId = id,
                CommandId = commandId,
            }).ToList();

            using var unitOfWork = ModApi.ServiceContainer.Resolve<IUnitOfWorkFactory>().Create();
            var levelGiftCommandRepository = unitOfWork.GetRepository<ILevelGiftCommandRepository>();
            await levelGiftCommandRepository.DeleteByLevelGiftIdAsync(id);
            await levelGiftCommandRepository.InsertAsync(entities);
            unitOfWork.Commit();

            return Ok();
        }
    }
}
