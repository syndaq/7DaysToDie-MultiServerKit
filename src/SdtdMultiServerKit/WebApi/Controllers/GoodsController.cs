using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace SdtdMultiServerKit.WebApi.Controllers
{
    /// <summary>
    /// Goods
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Goods")]
    public class GoodsController : ApiController
    {
        private readonly IGoodsRepository _goodsRepository;
        private readonly IItemListRepository _itemListRepository;
        private readonly ICommandListRepository _commandListRepository;

        /// <summary>
        /// Method
        /// </summary>
        /// <param name="goodsRepository"></param>
        /// <param name="itemListRepository"></param>
        /// <param name="commandListRepository"></param>
        public GoodsController(IGoodsRepository goodsRepository, IItemListRepository itemListRepository, ICommandListRepository commandListRepository)
        {
            _goodsRepository = goodsRepository;
            _itemListRepository = itemListRepository;
            _commandListRepository = commandListRepository;
        }

        /// <summary>
        /// IdGetrecord
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(T_Goods))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var entity = await _goodsRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        /// <summary>
        /// Getallrecord
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<T_Goods>> Get()
        {
            return await _goodsRepository.GetAllOrderByIdAsync();
        }

        /// <summary>
        /// Record
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Goods model)
        {
            var entity = new T_Goods()
            {
                Id = model.Id,
                Name = model.Name,
                CreatedAt = DateTime.Now,
                Price = model.Price,
                Description = model.Description,
            };
            await _goodsRepository.InsertAsync(entity);
            return Ok();
        }


        /// <summary>
        /// IdRecord
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] Goods model)
        {
            var entity = await _goodsRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = model.Name;
            entity.Price = model.Price;
            entity.Description = model.Description;
           
            await _goodsRepository.UpdateAsync(entity);
            return Ok();
        }

        /// <summary>
        /// IdDeleterecord
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            int count = await _goodsRepository.DeleteByIdAsync(id);
            if (count == 0)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// BatchDeleterecord
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="deleteAll"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> Delete([FromUri] int[]? ids, [FromUri] bool deleteAll = false)
        {
            int count = 0;

            if (deleteAll)
            {
               count = await _goodsRepository.DeleteAllAsync(true);
            }
            else if(ids != null && ids.Length > 0)
            {
                count = await _goodsRepository.DeleteByIdsAsync(ids, true);
            }

            return Ok(count);
        }

        /// <summary>
        /// Get item list associated with a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}/Items")]
        public async Task<IEnumerable<T_ItemList>> GetItems(int id)
        {
            var data = await _itemListRepository.GetListByGoodsIdAsync(id);
            return data;
        }

        /// <summary>
        /// Update items associated with a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemIds"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}/Items")]
        public async Task<IHttpActionResult> PutItems(int id, [FromBody, Required] int[] itemIds)
        {
            var entity = await _goodsRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var entities = new List<T_GoodsItem>();
            foreach (var item in itemIds)
            {
                entities.Add(new T_GoodsItem()
                {
                    GoodsId = id,
                    ItemId = item
                });
            }

            using var unitOfWork = ModApi.ServiceContainer.Resolve<IUnitOfWorkFactory>().Create();
            var userTagRepository = unitOfWork.GetRepository<IGoodsItemRepository>();
            await userTagRepository.DeleteByGoodsIdAsync(id);
            await userTagRepository.InsertAsync(entities);
            unitOfWork.Commit();

            return Ok();
        }

        /// <summary>
        /// Get commands associated with a productId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}/Commands")]
        public async Task<IEnumerable<T_CommandList>> GetCommands(int id)
        {
            var data = await _commandListRepository.GetListByGoodsIdAsync(id);
            return data;
        }

        /// <summary>
        /// Update commands associated with a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemIds"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}/Commands")]
        public async Task<IHttpActionResult> PutCommands(int id, [FromBody, Required] int[] itemIds)
        {
            var entity = await _goodsRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var entities = new List<T_GoodsCommand>();
            foreach (var item in itemIds)
            {
                entities.Add(new T_GoodsCommand()
                {
                    GoodsId = id,
                    CommandId = item
                });
            }

            using var unitOfWork = ModApi.ServiceContainer.Resolve<IUnitOfWorkFactory>().Create();
            var userTagRepository = unitOfWork.GetRepository<IGoodsCommandRepository>();
            await userTagRepository.DeleteByGoodsIdAsync(id);
            await userTagRepository.InsertAsync(entities);
            unitOfWork.Commit();

            return Ok();
        }
    }
}