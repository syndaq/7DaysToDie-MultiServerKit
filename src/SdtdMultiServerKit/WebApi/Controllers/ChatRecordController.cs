using System.ComponentModel.DataAnnotations;
using SdtdMultiServerKit.Data.IRepositories;
using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.Dtos;
using IceCoffee.SimpleCRUD.Dtos;

namespace SdtdMultiServerKit.WebApi.Controllers
{
    /// <summary>
    /// Record
    /// </summary>
    [Authorize]
    [RoutePrefix("api/ChatRecord")]
    public class ChatRecordController : ApiController
    {
        private readonly IChatRecordRepository _repository;

        /// <summary>
        /// Method
        /// </summary>
        /// <param name="chatRecordRepository"></param>
        public ChatRecordController(IChatRecordRepository chatRecordRepository) 
        {
            _repository = chatRecordRepository;
        }

        /// <summary>
        /// PaginationGetrecord
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<PagedDto<T_ChatRecord>> Get([FromUri] ChatRecordQuery model)
        {
            var dto = new ChatRecordQueryDto()
            {
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                StartDateTime = model.StartDateTime,
                EndDateTime = model.EndDateTime,
                Keyword = model.Keyword,
                Order = model.Order,
                Desc = model.Desc,
                ChatType = model.ChatType
            };
            var data = await _repository.GetPagedListAsync(dto);
            return data;
        }

        /// <summary>
        /// IdDeleterecord
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
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
        /// <param name="ids"></param>
        /// <param name="deleteAll"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> Delete([FromUri] string[]? ids, [FromUri] bool deleteAll = false)
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