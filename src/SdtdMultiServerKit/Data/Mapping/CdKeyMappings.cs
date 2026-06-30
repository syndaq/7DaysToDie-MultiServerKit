using SdtdMultiServerKit.Data.Dtos;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.Mapping
{
    internal static class CdKeyMappings
    {
        public static CdKeyDto ToDto(CdKey entity) => new()
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            Key = entity.Key,
            RedeemCount = entity.RedeemCount,
            MaxRedeemCount = entity.MaxRedeemCount,
            ExpiryAt = entity.ExpiryAt,
            Description = entity.Description,
        };

        public static IEnumerable<CdKeyDto> ToDtos(IEnumerable<CdKey> entities) =>
            entities.Select(ToDto);

        public static CdKey ToEntity(CdKeyCreateDto dto) => new()
        {
            Key = dto.Key,
            RedeemCount = dto.RedeemCount,
            MaxRedeemCount = dto.MaxRedeemCount,
            ExpiryAt = dto.ExpiryAt,
            Description = dto.Description,
        };

        public static void ApplyUpdate(CdKeyUpdateDto dto, CdKey entity)
        {
            entity.Key = dto.Key;
            entity.RedeemCount = dto.RedeemCount;
            entity.MaxRedeemCount = dto.MaxRedeemCount;
            entity.ExpiryAt = dto.ExpiryAt;
            entity.Description = dto.Description;
        }

        public static CdKeyRedeemRecordDto ToDto(CdKeyRedeemRecord entity) => new()
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            Key = entity.Key,
            PlayerId = entity.PlayerId,
            PlayerName = entity.PlayerName,
        };

        public static IEnumerable<CdKeyRedeemRecordDto> ToDtos(IEnumerable<CdKeyRedeemRecord> entities) =>
            entities.Select(ToDto);

        public static CdKeyRedeemRecord ToEntity(CdKeyRedeemRecordCreateDto dto) => new()
        {
            Key = dto.Key,
            PlayerId = dto.PlayerId,
            PlayerName = dto.PlayerName,
        };

        public static void ApplyUpdate(CdKeyRedeemRecordUpdateDto dto, CdKeyRedeemRecord entity)
        {
            entity.Key = dto.Key;
            entity.PlayerId = dto.PlayerId;
            entity.PlayerName = dto.PlayerName;
        }
    }
}
