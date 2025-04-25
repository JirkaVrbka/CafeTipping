using CaffeTipping.DbDomain.Repositories;
using CaffeTipping.DbInfrastructure.Contexts;
using CaffeTipping.DbModel;
using Microsoft.EntityFrameworkCore;

namespace CaffeTipping.DbInfrastructure.Repositories;

public class StatisticsRepository : GenericRepository<StatisticsEntity>, IStatisticsRepository
{
    public StatisticsRepository(IDbContextFactory<CaffeTippingContext> dbContextFactory) : base(dbContextFactory)
    {
    }

    public async Task<StatisticsEntity?> GetLatestStatistics()
    {
        await using var ctx = await DbContextFactory.CreateDbContextAsync();
        return ctx.StatisticsEntities.OrderByDescending(x => x.Updated).FirstOrDefault(); 
    }
}