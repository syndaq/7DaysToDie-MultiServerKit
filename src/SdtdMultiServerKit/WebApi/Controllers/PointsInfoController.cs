using IceCoffee.SimpleCRUD.Dtos;
using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;
using SdtdMultiServerKit.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace SdtdMultiServerKit.WebApi.Controllers
{
    /// <summary>
    /// Points info
    /// </summary>
    [Authorize]
    [RoutePrefix("api/PointsInfo")]
    public class PointsInfoController : ApiController
    {
        private readonly IPointsInfoRepository _repository;

        /// <summary>
        /// Method
        /// </summary>
        /// <param name="repository"></param>
        public PointsInfoController(IPointsInfoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// IdGetrecord
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(T_PointsInfo))]
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
        public async Task<PagedDto<T_PointsInfo>> Get([FromUri] PaginationQuery model)
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
        public async Task<IHttpActionResult> Post([FromBody] PointsInfo model)
        {
            using (Panel.PanelPointsSyncContext.EnterSuppressScope())
            {
                var entity = new T_PointsInfo()
                {
                    Id = model.Id,
                    CreatedAt = DateTime.Now,
                    LastSignInAt = model.LastSignInAt,
                    PlayerName = model.PlayerName,
                    Points = model.Points,
                };
                await _repository.InsertAsync(entity);
            }
            return Ok();
        }


        /// <summary>
        /// IdRecord
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(string id, [FromBody] PointsInfo model)
        {
            using (Panel.PanelPointsSyncContext.EnterSuppressScope())
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                {
                    return NotFound();
                }

                entity.LastSignInAt = model.LastSignInAt;
                entity.PlayerName = model.PlayerName;
                entity.Points = model.Points;
                await _repository.UpdateAsync(entity);
            }
            return Ok();
        }

        /// <summary>
        /// IdDeleterecord
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
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
        /// <param name="ids"></param>
        /// <param name="resetSignIn"></param>
        /// <param name="resetPoints"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> Delete([FromUri] string[]? ids, [FromUri] bool resetSignIn = false, [FromUri] bool resetPoints = false)
        {
            int count = 0;

            if (resetSignIn)
            {
                count = await _repository.ResetSignInAsync();
            }
            else if (resetPoints)
            {
                count = await _repository.ResetPointsAsync();
            }
            else if (ids != null && ids.Length > 0)
            {
                count = await _repository.DeleteByIdsAsync(ids, true);
            }


            return Ok(count);
        }
    }
}