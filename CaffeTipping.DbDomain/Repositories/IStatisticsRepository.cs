using CaffeTipping.DbModel;

namespace CaffeTipping.DbDomain.Repositories;

public interface IStatisticsRepository : IGenericRepository<StatisticsEntity>
{
    public Task<StatisticsEntity?> GetLatestStatistics();
}