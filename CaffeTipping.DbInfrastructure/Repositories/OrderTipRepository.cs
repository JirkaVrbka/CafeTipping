using CaffeTipping.DbDomain.Repositories;
using CaffeTipping.DbInfrastructure.Contexts;
using CaffeTipping.DbModel;
using Microsoft.EntityFrameworkCore;

namespace CaffeTipping.DbInfrastructure.Repositories;

public class OrderTipRepository : GenericRepository<OrderTipEntity>, IOrderTipRepository
{
    public OrderTipRepository(IDbContextFactory<CaffeTippingContext> dbContextFactory) : base(dbContextFactory)
    {
    }
}