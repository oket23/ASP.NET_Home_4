using Home_4.BLL.Enums;
using Home_4.BLL.Filters.Membership;
using Home_4.BLL.Interfaces.Repositories;
using Home_4.BLL.Models;
using Home_4.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Home_4.DAL.Repositories;

public class MembershipsRepository : BaseRepository<Membership>, IMembershipRepository
{
    public MembershipsRepository(HomeContext context) : base(context)
    {
    }
    
    public async Task<ResponseWrapper<Membership>> GetAllAsync(GetAllMembershipsFilter? filter)
    {
        filter ??= new GetAllMembershipsFilter();

        IQueryable<Membership> query = Set.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            query = query.Where(x => x.Title.ToLower().Contains(filter.Search.ToLower()));
        }
        
        if (filter.MemberId.HasValue)
        {
            query = query.Where(x => x.MemberId == filter.MemberId.Value);
        }
        
        if (filter.Status.HasValue)
        {
            query = query.Where(x => x.StatusEnum == filter.Status.Value);
        }
        
        if (filter.ExpirationDateFrom.HasValue)
        {
            query = query.Where(x => x.EndsOn >= filter.ExpirationDateFrom.Value);
        }

        if (filter.ExpirationDateTo.HasValue)
        {
            query = query.Where(x => x.EndsOn <= filter.ExpirationDateTo.Value);
        }
        
        int totalCount = await query.CountAsync();
        
        var items = await query
            .OrderByDescending(x => x.CreatedAtUtc)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new ResponseWrapper<Membership>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public async Task<Membership?> GetActiveByMemberIdAsync(int memberId)
    {
        return await Set.FirstOrDefaultAsync(x => 
            x.MemberId == memberId && 
            x.StatusEnum == MembershipStatusEnum.Active);
    }

    public async Task<bool> HasOverlappingMembershipAsync(int memberId, DateOnly start, DateOnly end, int? excludeMembershipId = null)
    {
        var query = Set.Where(x => x.MemberId == memberId && x.StatusEnum != MembershipStatusEnum.Expired);
        
        if (excludeMembershipId.HasValue)
        {
            query = query.Where(x => x.Id != excludeMembershipId.Value);
        }
        
        return await query.AnyAsync(x => x.StartsOn <= end && x.EndsOn >= start);
    }

    public async Task<IEnumerable<Membership>> GetAllByMemberIdAsync(int memberId)
    {
        return await Set
            .Where(x => x.MemberId == memberId)
            .OrderByDescending(x => x.EndsOn)
            .ToListAsync();
    }
}