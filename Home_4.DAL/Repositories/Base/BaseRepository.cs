using Home_4.BLL.Filters;
using Home_4.BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace Home_4.DAL.Repositories.Base;

public abstract class BaseRepository<T> where T : EntityBase
{
    protected readonly HomeContext Context;
    protected DbSet<T> Set => Context.Set<T>();

    protected BaseRepository(HomeContext context)
    {
        Context = context;
    }

    public virtual async Task<ResponseWrapper<T>> GetAllAsync(PaginationFilter? filter)
    {
        filter ??= new PaginationFilter();

        var query = Set.AsQueryable();

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new ResponseWrapper<T>()
        {
            Items =  items,
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public virtual async Task<T?> GetByIdAsync(int id)
        => await Set.FirstOrDefaultAsync(x => x.Id == id);

    public virtual async Task<T> CreateAsync(T entity)
    {
        Set.Add(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T?> UpdateAsync(T entity)
    {
        Set.Update(entity);
        await Context.SaveChangesAsync();
        return entity;
    }
    
    public virtual async Task<T?> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null)
        {
            return null;
        }

        Set.Remove(entity);
        await Context.SaveChangesAsync();
        return entity;
    }
}