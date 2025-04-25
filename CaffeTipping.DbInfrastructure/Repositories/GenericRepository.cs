using System.Linq.Expressions;
using CaffeTipping.DbDomain;
using CaffeTipping.DbInfrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CaffeTipping.DbInfrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly IDbContextFactory<CaffeTippingContext> DbContextFactory;

    protected GenericRepository(IDbContextFactory<CaffeTippingContext> dbContextFactory)
    {
        DbContextFactory = dbContextFactory;
    }

    public virtual void Add(T entity)
    {
        using var ctx = DbContextFactory.CreateDbContext();
        ctx.Set<T>().Add(entity);
        ctx.SaveChanges();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await using var ctx = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        ctx.Set<T>().Add(entity);
        await ctx.SaveChangesAsync(cancellationToken);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        using var ctx = DbContextFactory.CreateDbContext();
        ctx.Set<T>().AddRange(entities);
        ctx.SaveChanges();
    }

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await using var ctx = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        ctx.Set<T>().AddRange(entities);
        await ctx.SaveChangesAsync(cancellationToken);
    }

    public T? Get(Expression<Func<T, bool>> expression)
    {
        using var ctx = DbContextFactory.CreateDbContext();
        return ctx.Set<T>().FirstOrDefault(expression);
    }

    public T? Get(Guid id)
    {
        using var ctx = DbContextFactory.CreateDbContext(); 
        return ctx.Set<T>().Find(id); 
    }

    public async ValueTask<T?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var ctx = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        return await ctx.FindAsync<T>(id, cancellationToken);
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        await using var ctx = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        return await ctx
            .Set<T>()
            .FirstOrDefaultAsync(expression, cancellationToken);
    }

    public virtual List<T> GetAll()
    {
        using var ctx = DbContextFactory.CreateDbContext();
        return ctx.Set<T>().ToList();
    }

    public virtual List<T> GetAll(Expression<Func<T, bool>> expression)
    {
        using var ctx = DbContextFactory.CreateDbContext();
        return ctx
            .Set<T>()
            .Where(expression)
			.AsSplitQuery()
			.ToList();
    }

    public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await using var ctx = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        return await ctx.Set<T>().AsSplitQuery().ToListAsync(cancellationToken);
    }

    public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        await using var ctx = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        return await ctx
            .Set<T>()
            .Where(expression)
            .ToListAsync(cancellationToken);
    }

    public void Remove(T entity)
    {
        using var ctx = DbContextFactory.CreateDbContext();
        ctx.Set<T>().Remove(entity);
        ctx.SaveChanges();
    }

    public async Task RemoveAsync(T entity)
    {
        await using var ctx = await DbContextFactory.CreateDbContextAsync();
        ctx.Set<T>().Remove(entity);
        await ctx.SaveChangesAsync();
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        using var ctx = DbContextFactory.CreateDbContext();
        ctx.Set<T>().RemoveRange(entities);
        ctx.SaveChanges();
    }

    public async Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        await using var ctx = await DbContextFactory.CreateDbContextAsync();
        ctx.Set<T>().RemoveRange(entities);
        await ctx.SaveChangesAsync();
    }

    public virtual void Update(T entity)
    {
        using var ctx = DbContextFactory.CreateDbContext();
        ctx.Set<T>().Update(entity);
        ctx.SaveChanges();
    }
    
    public virtual async Task UpdateAsync(T entity)
    {
        await using var ctx = await DbContextFactory.CreateDbContextAsync();
        ctx.Set<T>().Update(entity);
        await ctx.SaveChangesAsync();
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        using var ctx = DbContextFactory.CreateDbContext();
        ctx.Set<T>().UpdateRange(entities);
        ctx.SaveChanges();
    }
    
    public async Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        await using var ctx = await DbContextFactory.CreateDbContextAsync();
        ctx.Set<T>().UpdateRange(entities);
        await ctx.SaveChangesAsync();
    }
}