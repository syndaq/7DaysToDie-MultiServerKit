using IceCoffee.SimpleCRUD.Dtos;
using SdtdMultiServerKit.Data.Dtos;
using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;
using System.Text;

namespace SdtdMultiServerKit.Data.Repositories
{
    public class ChatRecordRepository : DefaultRepository<T_ChatRecord>, IChatRecordRepository
    {
        public Task<PagedDto<T_ChatRecord>> GetPagedListAsync(ChatRecordQueryDto dto)
        {
            var whereClauseSB = new StringBuilder("1=1");

            if (dto.StartDateTime.HasValue)
            {
                whereClauseSB.Append($" AND CreatedAt>=@StartDateTime");
            }

            if (dto.EndDateTime.HasValue)
            {
                whereClauseSB.Append($" AND CreatedAt<=@EndDateTime");
            }

            if(dto.ChatType.HasValue)
            {
                whereClauseSB.Append(" AND ChatType=@ChatType");
            }

            if (string.IsNullOrEmpty(dto.Keyword) == false)
            {
                whereClauseSB.Append(" AND (EntityId=@Keyword OR PlayerId=@Keyword OR SenderName LIKE '%'||@Keyword||'%' OR Message LIKE '%'||@Keyword||'%')");
            }

            string orderByClause = dto.Order + (dto.Desc ? " DESC" : " ASC");

            var param = new { Keyword = dto.Keyword, StartDateTime = dto.StartDateTime, EndDateTime = dto.EndDateTime, ChatType = dto.ChatType };
            return base.GetPagedListAsync(dto.PageNumber, dto.PageSize, whereClauseSB.ToString(), orderByClause, param);
        }
    }
}