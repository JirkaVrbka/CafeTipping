using CaffeTipping.DbDomain.Repositories;
using CaffeTipping.DbModel;
using CaffeTipping.DbServices.Dtos;
using CaffeTipping.DbServices.Mappers;

namespace CaffeTipping.DbServices.Services;

public class StatisticsService(IStatisticsRepository statisticsRepository) : IStatisticsService
{
    private readonly IStatisticsRepository _statisticsRepository = statisticsRepository;


    public async Task UpdateStatistics(StatisticsDto statisticsDto)
    {

        Console.WriteLine($"Updating statistics: {statisticsDto}");
        var entity = await _statisticsRepository.GetAsync(statisticsDto.Id);
        if (entity is null)
        {
            Console.WriteLine($"Statistics not found: {statisticsDto}");
            return;
        }

        entity.Updated = DateTime.Now;
        entity.Inserted = statisticsDto.Inserted;
        entity.HighestTipOfDay = statisticsDto.HighestTipOfDay;
        entity.AverageRating = statisticsDto.AverageRating;
        entity.AverageTipPerc = statisticsDto.AverageTipPerc;
        entity.TotalTips = statisticsDto.TotalTips;

        Console.WriteLine("Updating statistics");
        await _statisticsRepository.UpdateAsync(entity);
    }

    public async Task<StatisticsDto> GetLatestAsync()
    {
        var all = await _statisticsRepository.GetAllAsync();
        Console.WriteLine($"Retrieved statistics: {all.Count}");
        
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