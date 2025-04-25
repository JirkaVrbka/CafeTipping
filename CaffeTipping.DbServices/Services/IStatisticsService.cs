using CaffeTipping.DbServices.Dtos;

namespace CaffeTipping.DbServices.Services;

public interface IStatisticsService
{
    public Task UpdateStatistics(StatisticsDto statisticsDto);
    public Task<StatisticsDto> GetLatestAsync();
}