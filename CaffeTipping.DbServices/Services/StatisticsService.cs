using CaffeTipping.DbDomain.Repositories;
using CaffeTipping.DbModel;
using CaffeTipping.DbServices.Mappers;
using CaffeTipping.ServicesContract;
using CaffeTipping.ServicesContract.Dtos;
using Microsoft.Extensions.Logging;

namespace CaffeTipping.DbServices.Services;

public class StatisticsService(ILogger<StatisticsService> logger, IStatisticsRepository statisticsRepository) : IStatisticsService
{
    private readonly IStatisticsRepository _statisticsRepository = statisticsRepository;


    public async Task UpdateStatistics(StatisticsDto statisticsDto)
    {
        var entity = await _statisticsRepository.GetAsync(statisticsDto.Id);
        if (entity is null)
        {
            logger.LogWarning("Unable to update statistics dto");
            return;
        }

        entity.Updated = DateTime.Now;
        entity.Inserted = statisticsDto.Inserted;
        entity.HighestTipOfDay = statisticsDto.HighestTipOfDay;
        entity.AverageRating = statisticsDto.AverageRating;
        entity.AverageTipPerc = statisticsDto.AverageTipPerc;
        entity.TotalTips = statisticsDto.TotalTips;

        await _statisticsRepository.UpdateAsync(entity);
    }

    public async Task<StatisticsDto> GetLatestAsync()
    {
        var latest = await _statisticsRepository.GetLatestStatistics();
        if (latest != null && latest.Updated.Date.Equals(DateTime.Today))
            return latest.ToDto();


        var stats = new StatisticsEntity()
        {
            Inserted = DateOnly.FromDateTime(DateTime.Today),
            Updated = DateTime.Now,
            TotalTips = 0,
            HighestTipOfDay = Guid.Empty,
            AverageTipPerc = 0,
        };
        await _statisticsRepository.AddAsync(stats);
        return  (await _statisticsRepository.GetLatestStatistics())!.ToDto();

    }
    
    
}