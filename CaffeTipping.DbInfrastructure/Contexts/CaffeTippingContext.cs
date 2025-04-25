using CaffeTipping.DbModel;
using Microsoft.EntityFrameworkCore;

namespace CaffeTipping.DbInfrastructure.Contexts;

public class CaffeTippingContext(DbContextOptions<CaffeTippingContext> options)
    : DbContext(options), ICaffeTippingContext
{
    public DbSet<OrderTipEntity> OrderTips { get; set; }
    public DbSet<StatisticsEntity> StatisticsEntities { get; set; }
}