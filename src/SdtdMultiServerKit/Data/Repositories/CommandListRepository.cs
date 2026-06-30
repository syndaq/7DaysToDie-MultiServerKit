using IceCoffee.SimpleCRUD.Dtos;
using IceCoffee.SimpleCRUD.SqlGenerators;
using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;
using System.Text;

namespace SdtdMultiServerKit.Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandListRepository : DefaultRepository<T_CommandList>, ICommandListRepository
    {
        public Task<IEnumerable<T_CommandList>> GetListByCdKeyIdAsync(int cdKeyId)
        {
            string whereClause = "Id IN (SELECT CommandId FROM CdKeyCommand WHERE CdKeyId=@CdKeyId)";
            return base.GetListAsync(whereClause, param: new { CdKeyId = cdKeyId });
        }

        /// <inheritdoc/>
        public Task<IEnumerable<T_CommandList>> GetListByGoodsIdAsync(int goodsId)
        {
            string whereClause = "Id IN (SELECT CommandId FROM T_GoodsCommand WHERE GoodsId=@GoodsId)";
            return base.GetListAsync(whereClause, param: new { GoodsId = goodsId });
        }

        public Task<IEnumerable<T_CommandList>> GetListByTaskScheduleIdAsync(int taskScheduleId)
        {
            string whereClause = "Id IN (SELECT CommandId FROM T_TaskScheduleCommand WHERE TaskScheduleId=@TaskScheduleId)";
            return base.GetListAsync(whereClause, param: new { TaskScheduleId = taskScheduleId });
        }

        /// <inheritdoc/>
        public Task<IEnumerable<T_CommandList>> GetListByVipGiftIdAsync(string vipGiftId)
        {
            string tableName = GetSqlGenerator<T_VipGiftCommand>().TableName;
            string whereClause = $"Id IN (SELECT CommandId FROM {tableName} WHERE VipGiftId=@VipGiftId)";
            return base.GetListAsync(whereClause, param: new { VipGiftId = vipGiftId });
        }

        public Task<IEnumerable<T_CommandList>> GetListByLevelGiftIdAsync(string levelGiftId)
        {
            string tableName = GetSqlGenerator<T_LevelGiftCommand>().TableName;
            string whereClause = $"Id IN (SELECT CommandId FROM {tableName} WHERE LevelGiftId=@LevelGiftId)";
            return base.GetListAsync(whereClause, param: new { LevelGiftId = levelGiftId });
        }

        public Task<IEnumerable<T_CommandList>> GetListByLotteryPoolIdAsync(int lotteryPoolId)
        {
            string whereClause = "Id IN (SELECT CommandId FROM T_LotteryPoolCommand WHERE LotteryPoolId=@LotteryPoolId)";
            return base.GetListAsync(whereClause, param: new { LotteryPoolId = lotteryPoolId });
        }

        /// <inheritdoc/>
        public Task<PagedDto<T_CommandList>> GetPagedListAsync(PaginationQueryDto dto)
        {
            var whereClauseSB = new StringBuilder("1=1");

            if (string.IsNullOrEmpty(dto.Keyword) == false)
            {
                whereClauseSB.Append(" AND (Id=@Keyword OR Command LIKE '%'||@Keyword||'%' OR Description LIKE '%'||@Keyword||'%')");
            }

            var param = new { Keyword = dto.Keyword };
            return GetPagedListAsync(dto.PageNumber, dto.PageSize, whereClauseSB.ToString(), orderByClause: "Id DESC", param);
        }
    }
}