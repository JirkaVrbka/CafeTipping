using CaffeTipping.ServicesContract.Dtos;

namespace CaffeTipping.ServicesContract;

public interface IStatisticsService
{
    public Task UpdateStatistics(StatisticsDto statisticsDto);
    public Task<StatisticsDto> GetLatestAsync();
}