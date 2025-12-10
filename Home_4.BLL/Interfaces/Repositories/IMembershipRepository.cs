using Home_4.BLL.Filters.Membership;
using Home_4.BLL.Models;

namespace Home_4.BLL.Interfaces.Repositories;

public interface IMembershipRepository
{
    Task<ResponseWrapper<Membership>> GetAllAsync(GetAllMembershipsFilter? filter);
    Task<Membership?> GetByIdAsync(int id);
    Task<Membership> CreateAsync(Membership membership);
    Task<Membership?> UpdateAsync(Membership membership);
    Task<Membership?> DeleteAsync(int id);
    
    Task<Membership?> GetActiveByMemberIdAsync(int memberId);
    Task<bool> HasOverlappingMembershipAsync(int memberId, DateOnly start, DateOnly end, int? excludeMembershipId = null);
    Task<IEnumerable<Membership>> GetAllByMemberIdAsync(int memberId);
}