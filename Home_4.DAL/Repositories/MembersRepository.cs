using Home_4.BLL.Filters;
using Home_4.BLL.Interfaces.Repositories;
using Home_4.BLL.Models;
using Home_4.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Home_4.DAL.Repositories;

public class MembersRepository : BaseRepository<Member>, IMemberRepository 
{
    public MembersRepository(HomeContext context) : base(context)
    {
    }
    
    public async Task<ResponseWrapper<Member>> GetAllAsync(GetAllMembersFilter? filter = null)
    {
        filter ??= new GetAllMembersFilter(); 

        IQueryable<Member> query = Set.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            query = query.Where(x => x.Email.ToLower().Contains(filter.Search.ToLower())
                                     || x.FirstName.ToLower().Contains(filter.Search.ToLower()) 
                                     || x.LastName.ToLower().Contains(filter.Search.ToLower()));
        }

        if (filter.IsActive.HasValue)
        {
            query = query.Where(x => x.IsActive == filter.IsActive.Value);
        }
        
        int totalCount = await query.CountAsync();
        
        var items = await query
            .OrderBy(x => x.Id)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync(); 
        
        return new ResponseWrapper<Member>()
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize 
        };
    }
    
    public async Task<bool> EmailExistsAsync(string email)
    {
        return await Set.AnyAsync(x => x.Email == email);
    }
    
    public async Task<Member?> GetByEmailAsync(string email)
    {
        return await Set.FirstOrDefaultAsync(x => x.Email == email);
    }
    
    public async Task<Member?> DeactivateAsync(int id, string? reason)
    {
        var member = await GetByIdAsync(id);
        if (member == null)
        {
            return null;
        }

        member.IsActive = false;
        member.DeactivateReason = (reason == null) ? "Unluck" : reason;
        
        await UpdateAsync(member); 
        
        return member;
    }
}