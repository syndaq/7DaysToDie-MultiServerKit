using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace SdtdMultiServerKit.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/LotteryPool")]
    public class LotteryPoolController : ApiController
    {
        private readonly ILotteryPoolRepository _lotteryPoolRepository;
        private readonly IItemListRepository _itemListRepository;
        private readonly ICommandListRepository _commandListRepository;

        public LotteryPoolController(
            ILotteryPoolRepository lotteryPoolRepository,
            IItemListRepository itemListRepository,
            ICommandListRepository commandListRepository)
        {
            _lotteryPoolRepository = lotteryPoolRepository;
            _itemListRepository = itemListRepository;
            _commandListRepository = commandListRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(T_LotteryPool))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var entity = await _lotteryPoolRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<T_LotteryPool>> Get()
        {
            return await _lotteryPoolRepository.GetAllOrderByIdAsync();
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] LotteryPool model)
        {
            var entity = new T_LotteryPool()
            {
                Id = model.Id,
                Name = model.Name,
                CreatedAt = DateTime.Now,
                DrawCost = model.DrawCost,
                Weight = model.Weight,
                IsEnabled = model.IsEnabled,
                Description = model.Description,
            };
            await _lotteryPoolRepository.InsertAsync(entity);
            return Ok();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] LotteryPool model)
        {
            var entity = await _lotteryPoolRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = model.Name;
            entity.DrawCost = model.DrawCost;
            entity.Weight = model.Weight;
            entity.IsEnabled = model.IsEnabled;
            entity.Description = model.Description;

            await _lotteryPoolRepository.UpdateAsync(entity);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            int count = await _lotteryPoolRepository.DeleteByIdAsync(id);
            if (count == 0)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> Delete([FromUri] int[]? ids, [FromUri] bool deleteAll = false)
        {
            int count = 0;

            if (deleteAll)
            {
                count = await _lotteryPoolRepository.DeleteAllAsync(true);
            }
            else if (ids != null && ids.Length > 0)
            {
                count = await _lotteryPoolRepository.DeleteByIdsAsync(ids, true);
            }

            return Ok(count);
        }

        [HttpGet]
        [Route("{id:int}/Items")]
        public async Task<IEnumerable<T_ItemList>> GetItems(int id)
        {
            return await _itemListRepository.GetListByLotteryPoolIdAsync(id);
        }

        [HttpPut]
        [Route("{id:int}/Items")]
        public async Task<IHttpActionResult> PutItems(int id, [FromBody, Required] int[] itemIds)
        {
            var entity = await _lotteryPoolRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var entities = itemIds.Select(itemId => new T_LotteryPoolItem
            {
                LotteryPoolId = id,
                ItemId = itemId,
            }).ToList();

            using var unitOfWork = ModApi.ServiceContainer.Resolve<IUnitOfWorkFactory>().Create();
            var repository = unitOfWork.GetRepository<ILotteryPoolItemRepository>();
            await repository.DeleteByLotteryPoolIdAsync(id);
            await repository.InsertAsync(entities);
            unitOfWork.Commit();

            return Ok();
        }

        [HttpGet]
        [Route("{id:int}/Commands")]
        public async Task<IEnumerable<T_CommandList>> GetCommands(int id)
        {
            return await _commandListRepository.GetListByLotteryPoolIdAsync(id);
        }

        [HttpPut]
        [Route("{id:int}/Commands")]
        public async Task<IHttpActionResult> PutCommands(int id, [FromBody, Required] int[] itemIds)
        {
            var entity = await _lotteryPoolRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var entities = itemIds.Select(commandId => new T_LotteryPoolCommand
            {
                LotteryPoolId = id,
                CommandId = commandId,
            }).ToList();

            using var unitOfWork = ModApi.ServiceContainer.Resolve<IUnitOfWorkFactory>().Create();
            var repository = unitOfWork.GetRepository<ILotteryPoolCommandRepository>();
            await repository.DeleteByLotteryPoolIdAsync(id);
            await repository.InsertAsync(entities);
            unitOfWork.Commit();

            return Ok();
        }
    }
}
