using CaffeTipping.ServicesContract;
using CaffeTipping.ServicesContract.Dtos;
using Microsoft.Extensions.Logging;

namespace CaffeTipping.FileServices;

public class FileStatisticsService(ILogger<FileStatisticsService> logger) : GenericFileService<StatisticsDto>(logger), IStatisticsService
{
    protected override string FileName { get; } = "Statistics.json";
    
    public async Task UpdateStatistics(StatisticsDto statisticsDto)
    {
        var all = await GetFromFile();
        var stats = all.FirstOrDefault(o => o.Id.Equals(statisticsDto.Id));

        if (stats is null)
        {
            logger.LogWarning("Unable to update statistics dto");
            return;
        }
        else
        {
            all[all.IndexOf(stats)] = statisticsDto;
        }
        
        await WriteToFile(all);
    }

    public async Task<StatisticsDto> GetLatestAsync()
    {
        var all = await GetFromFile();
        var latest = all.FirstOrDefault(s => s.Updated.Date.Equals(DateTime.Today));
        if (latest != null)
            return latest;


        var stats = new StatisticsDto()
        {
            Id = Guid.NewGuid(),
            Inserted = DateOnly.FromDateTime(DateTime.Today),
            Updated = DateTime.Now,
            TotalTips = 0,
            HighestTipOfDay = Guid.Empty,
            AverageTipPerc = 0,
        };
       
        all.Add(stats);
        await WriteToFile(all);
        return stats;
    }
}