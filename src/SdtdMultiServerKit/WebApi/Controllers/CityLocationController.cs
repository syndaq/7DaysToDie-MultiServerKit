using System.ComponentModel.DataAnnotations;
using SdtdMultiServerKit.Data.IRepositories;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("api/CityLocation")]
    public class CityLocationController : ApiController
    {
        private readonly ICityLocationRepository _repository;

        /// <summary>
        /// Method
        /// </summary>
        /// <param name="repository"></param>
        public CityLocationController(ICityLocationRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// IdGetrecord
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(T_CityLocation))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
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
        public async Task<IEnumerable<T_CityLocation>> Get()
        {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// Record
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] CityLocation model)
        {
            var entity = new T_CityLocation()
            {
                Id = model.Id,
                CityName = model.CityName,
                CreatedAt = DateTime.Now,
                PointsRequired = model.PointsRequired,
                Position = model.Position,
                ViewDirection = model.ViewDirection
            };
            await _repository.InsertAsync(entity);
            return Ok();
        }


        /// <summary>
        /// IdRecord
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] CityLocation model)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.CityName = model.CityName;
            entity.PointsRequired = model.PointsRequired;
            entity.Position = model.Position;
            entity.ViewDirection = model.ViewDirection;
            await _repository.UpdateAsync(entity);
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
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> Delete([FromUri, MinLength(1)] int[] ids)
        {
            int count = await _repository.DeleteByIdsAsync(ids, true);
            return Ok(count);
        }
    }
}