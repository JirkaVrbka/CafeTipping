using CaffeTipping.DbModel;
using CaffeTipping.DbServices.Dtos;

namespace CaffeTipping.DbServices.Mappers;

public static class StatisticsMapper
{
    public static StatisticsDto ToDto(this StatisticsEntity entity)
        => new()
        {
            Id = entity.Id,
            Inserted = entity.Inserted,
            Updated = entity.Updated,
            TotalTips = entity.TotalTips,
            AverageRating = entity.AverageRating,
            AverageTipPerc = entity.AverageTipPerc,
            HighestTipOfDay = entity.HighestTipOfDay,
        };

    public static StatisticsEntity ToEntity(this StatisticsDto dto)
        => new()
        {
            Id = dto.Id,
            Inserted = dto.Inserted,
            Updated = dto.Updated,
            TotalTips = dto.TotalTips,
            AverageRating = dto.AverageRating,
            AverageTipPerc = dto.AverageTipPerc,
            HighestTipOfDay = dto.HighestTipOfDay,
        }; 
}