using CaffeTipping.DbModel;
using Microsoft.EntityFrameworkCore;

namespace CaffeTipping.DbInfrastructure.Contexts;

public interface ICaffeTippingContext
{
    DbSet<OrderTipEntity> OrderTips { get; set; }
}