using IceCoffee.SimpleCRUD.Dtos;
using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;
using SdtdMultiServerKit.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace SdtdMultiServerKit.WebApi.Controllers
{
    /// <summary>
    /// Itemlist
    /// </summary>
    [Authorize]
    [RoutePrefix("api/ItemList")]
    public class ItemListController : ApiController
    {
        private readonly IItemListRepository _repository;

        /// <summary>
        /// Method
        /// </summary>
        /// <param name="repository"></param>
        public ItemListController(IItemListRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// IdGetrecord
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(T_ItemList))]
        public async Task<IHttpActionResult> Get(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        /// <summary>
        /// PaginationGetrecord
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<PagedDto<T_ItemList>> Get([FromUri] PaginationQuery model)
        {
            var dto = new PaginationQueryDto()
            {
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                Keyword = model.Keyword,
            };
            return await _repository.GetPagedListAsync(dto);
        }

        /// <summary>
        /// Record
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody, Required] ItemList model)
        {
            var entity = new T_ItemList()
            {
                CreatedAt = DateTime.Now,
                Count = model.Count,
                Durability = model.Durability,
                ItemName = model.ItemName,
                Quality = model.Quality,
                Description = model.Description,
            };

            int id = await _repository.InsertAsync<int>(entity);
            return Ok(id);
        }

        /// <summary>
        /// IdRecord
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(string id, [FromBody, Required] ItemList model)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Count = model.Count;
            entity.Durability = model.Durability;
            entity.ItemName = model.ItemName;
            entity.Quality = model.Quality;
            entity.Description = model.Description;

            await _repository.UpdateAsync(entity);
            return Ok();
        }

        /// <summary>
        /// IdDeleterecord
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(string id)
        {
            int count = await _repository.DeleteByIdAsync(id);
            if (count == 0)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// BatchDeleterecord
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> Delete([FromUri] int[]? ids, [FromUri] bool deleteAll = false)
        {
            int count = 0;

            if (deleteAll)
            {
                count = await _repository.DeleteAllAsync(true);
            }
            else if (ids != null && ids.Length > 0)
            {
                count = await _repository.DeleteByIdsAsync(ids, true);
            }

            return Ok(count);
        }
    }
}